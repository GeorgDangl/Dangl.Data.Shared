using System.Collections.Generic;

namespace Dangl.Data.Shared
{
    public class ApiError
    {
        public virtual Dictionary<string, string[]> Errors { get; }
    }
}
