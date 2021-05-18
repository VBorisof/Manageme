using System;

namespace Manageme.Data.Models
{
    public interface IModelBase
    {
        long Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
    }
}