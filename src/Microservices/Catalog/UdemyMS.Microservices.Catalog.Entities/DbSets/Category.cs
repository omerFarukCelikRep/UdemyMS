namespace UdemyMS.Microservices.Catalog.Entities.DbSets;
public class Category : BaseEntity<ObjectId>
{
    public string Name { get; set; }
}