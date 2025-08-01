namespace Stockly.Infrastructure.Adapter.FirebaseDb.Constants;

public static class FirebaseConstants
{
    public const string Issuer = "stockly-api";
    public const string Audience = "stockly-client";

    public static class Properties
    {
        public const string Name = "name";
        public const string Email = "email";
        public const string Category = "category";
        public const string Stock = "stock";
        public const string Price = "price";
        public const string StatusColor = "statusColor";
        public const string CreatedAt = "createdAt";
    }
}