using System.Globalization;
using Domain.Common;
using SharedKernel;

namespace Domain.Complaint;

public class ComplaintType : Entity, IAuditableBase
{
    public string Name { get; protected set; }
    public int Code { get; protected set; }

    private readonly List<ComplaintSubType> _subTypes = new();
    public IReadOnlyList<ComplaintSubType> SubTypes => _subTypes.AsReadOnly();
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public string ConsumerId { get; set; }

    protected ComplaintType() : base() { }

    // Constructor with all parameters (for reconstruction from persistence)
    public ComplaintType(
        string name,
        int code,
        string createdBy,
        DateTime createdDate,
        string? modifiedBy = null,
        DateTime? modifiedDate = null,
        string? id = null,
        IEnumerable<ComplaintSubType>? subTypes = null)
    {
        SetName(name);
        SetCode(code);

        CreatedDate = createdDate;
        ModifiedBy = modifiedBy ?? "";
        ModifiedDate = modifiedDate;

        if (subTypes != null)
        {
            _subTypes.AddRange(subTypes);
        }

        if (string.IsNullOrEmpty(id))
        {
            //AddDomainEvent(new ComplaintTypeCreatedEvent(Id, Name, Code, CreatedBy));
        }
    }

    // Business methods with validation
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException($"{name} Complaint type name cannot be empty");
        }


        if (name.Length > 100)
        {
            throw new ArgumentNullException($"{name} Complaint type name cannot exceed 100 characters");
        }

        if (Name != name)
        {
            Name = name;
            UpdateAuditFields();
        }
    }

    public void SetCode(int code)
    {
        if (code <= 0)
        {
            throw new ArgumentNullException($"{code} Complaint type code must be positive");
        }


        if (Code != code)
        {
            Code = code;
            UpdateAuditFields();
        }
    }

    public void AddSubType(string name, int code, string createdBy, DateTime createdDate, string? modifiedBy, DateTime? modifiedDate, string? id = null)
    {
        if (SubTypes.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) != null)
        {
            return;
        }

        var subType = new ComplaintSubType(this, name, code, createdBy, createdDate, modifiedBy, modifiedDate, id);
        _subTypes.Add(subType);

        //if (id == null)
        //    AddDomainEvent(new ComplaintSubTypeAddedToComplaintTypeEvent(subType.Id, subType.Name, subType.Code, subType.ComplainType.Id, subType.CreatedBy));
    }

    // SubType management methods
    public void AddSubType(ComplaintSubType subType, string modifiedBy)
    {
        ArgumentNullException.ThrowIfNull(subType);

        _subTypes.Add(subType);
        UpdateAuditFields(modifiedBy);

        //AddDomainEvent(new ComplaintSubTypeAddedEvent(Id, subType.Id, subType.Name));
    }

    public void RemoveSubType(string subTypeId, string modifiedBy)
    {
        ComplaintSubType? subType = _subTypes.FirstOrDefault(st => st.Id == subTypeId) ?? throw new ArgumentNullException($"SubType with id {subTypeId} not found");


        _subTypes.Remove(subType);
        UpdateAuditFields(modifiedBy);

        // AddDomainEvent(new ComplaintSubTypeRemovedEvent(Id, subTypeId, subType.Name));
    }

    public ComplaintSubType? GetSubTypeById(string id)
    {
        return _subTypes.FirstOrDefault(st => st.Id == id);
    }

    public ComplaintSubType? GetSubTypeByCode(int code)
    {
        return _subTypes.FirstOrDefault(st => st.Code == code);
    }

    public bool HasSubType(string subTypeId)
    {
        return _subTypes.Any(st => st.Id == subTypeId);
    }

    // Helper method to update audit fields
    private void UpdateAuditFields(string? modifiedBy = null)
    {
        ModifiedDate = DateTime.UtcNow;
        ModifiedBy = modifiedBy ?? ModifiedBy;
    }
}
