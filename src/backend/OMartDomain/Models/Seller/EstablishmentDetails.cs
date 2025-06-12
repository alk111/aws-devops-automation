public class EstablishmentDetails
{
    public string user_id { get; set; } = string.Empty; // Default value: empty string
    public Guid entity_id { get; set; } = Guid.NewGuid(); // Default value: new Guid
    public int iterations { get; set; } = 0; // Default value: 0
    public string establishment_type { get; set; } = string.Empty; // Default value: empty string
    public string establishment_name { get; set; } = string.Empty; // Default value: empty string
    public string address { get; set; } = string.Empty; // Default value: empty string
    public string contact_no_1 { get; set; } = string.Empty; // Default value: empty string
    public string contact_no_2 { get; set; } = string.Empty; // Default value: empty string
    public string contact_email_1 { get; set; } = string.Empty; // Default value: empty string
    public string contact_email_2 { get; set; } = string.Empty; // Default value: empty string
    public string gmaps_location { get; set; } = string.Empty; // Default value: empty string
    public TimeSpan? open_time { get; set; } = null; // Nullable time
    public TimeSpan? close_time { get; set; } = null; // Nullable time
    public string holidays { get; set; } = string.Empty; // Default value: empty string
    public bool contact_1_2fa_verified { get; set; } = false; // Default value: false
    public bool contact_2_2fa_verified { get; set; } = false; // Default value: false
    public DateTime registered_on { get; set; } = DateTime.UtcNow; // Default value: current UTC time
    public DateTime? updated_on { get; set; } = null; // Nullable date
    public bool is_active { get; set; } = true; // Default value: true
    public bool is_deleted { get; set; } = false; // Default value: false

    // Additional fields
    public decimal minimum_payment { get; set; } = 0.00M; // Default value: 0.00
    public int instant_del_duration_min { get; set; } = 0; // Default value: 0
    public string payment_mode { get; set; } = string.Empty; // Default value: empty string
}
