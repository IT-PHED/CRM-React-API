namespace Application.Abstractions.Factory;

public interface IRazorViewToString
{
    Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
}
