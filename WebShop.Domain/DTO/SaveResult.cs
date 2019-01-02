using System.Collections.Generic;

namespace WebShop.Domain.DTO
{
    public class SaveResult
    {
        public bool IsSuccess { get; set; }

        public IEnumerable<string> Errors { get; set; }

    }
}
