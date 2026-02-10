using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Abstractions.Factory;
using Microsoft.Extensions.Configuration;
using SharedKernel;

namespace Infrastructure.Factory;

internal sealed class SmsService(
    IHttpClientFactory httpClientFactory,
    IDateTimeProvider dateTimeProvider,
    IConfiguration configuration) : ISmsService
{
    public async Task SendSms(string Message, string PhoneNumber, CancellationToken cancellationToken = default)
    {
        try
        {
            using HttpClient client = httpClientFactory.CreateClient();
            string recipientNumber = vetPhoneNumber(PhoneNumber);
            string smsUrl = configuration["SMS:URL"];
            string smsBearerToken = configuration["SMS:BearerToken"];
            string timestamp = dateTimeProvider.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", smsBearerToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonContent = new
            {
                senderID = "PHED Cares",
                messageText = Message,
                deliveryTime = timestamp,
                mobileNumber = recipientNumber
            };

            HttpResponseMessage response = await client.PostAsJsonAsync(smsUrl, jsonContent, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.RequestMessage);
            }
        }
        catch (Exception ex)
        {
            /// even if it fails don't throw exception to disrupt the request pipeline, just log exception gracefully
            Console.WriteLine(ex.ToString());
        }
    }

    #region helpers
    private string vetPhoneNumber(string phoneNumber)
    {
        if (phoneNumber == null)
        {
            return "0";
        }

        string trimmedNumber = phoneNumber.Trim();


        bool isNumeric = true;
        for (int i = trimmedNumber.StartsWith('+') ? 1 : 0; i < trimmedNumber.Length; i++)
        {
            if (!char.IsDigit(trimmedNumber[i]))
            {
                isNumeric = false;
                break;
            }
        }

        if (!isNumeric)
        { return "0"; }

        if (trimmedNumber.StartsWith("+234", StringComparison.CurrentCulture) && trimmedNumber.Length == 14)
        {
            return trimmedNumber;
        }
        else if (trimmedNumber.StartsWith("234", StringComparison.CurrentCulture) && trimmedNumber.Length == 13)
        {
            return trimmedNumber;
        }
        else if (trimmedNumber.StartsWith('0') && trimmedNumber.Length == 11)
        {
            return string.Concat("+234", trimmedNumber.AsSpan(1, trimmedNumber.Length - 1));
        }
        else if (!trimmedNumber.StartsWith('+') && !trimmedNumber.StartsWith("234", StringComparison.CurrentCulture) && !trimmedNumber.StartsWith('0') && trimmedNumber.Length == 10)
        {

            return "0" + trimmedNumber;

        }

        return "0";
    }
    #endregion
}
