namespace PostOfficeBackend.Dtos
{
    public class ParcelDto
    {
        public int Id { get; set; }
        public decimal Weight { get; set; }
        public string Phone { get; set; }
        public string Info { get; set; }
        public int? PostId { get; set; }
    }
}
