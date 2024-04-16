using Microsoft.AspNetCore.Mvc;

namespace API.DTOs.TransactionEvents
{
    public class UploadImageDto
    {
        public Guid Guid { get; set; }

        public string imagePath { get; set; }
    }
}
