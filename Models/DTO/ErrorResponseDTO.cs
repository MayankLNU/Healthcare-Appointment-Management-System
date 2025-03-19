namespace AppointmentManagement.Models.DTO
{
    public class ErrorResponseDTO
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
