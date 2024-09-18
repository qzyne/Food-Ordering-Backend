namespace OrderingFoodFinalTerm.DTO
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string? MenuName { get; set; }
        public string? MenuDescription { get; set; }
        public int IsActive { get; set; } // value 1:active - 0:inactive
    }
}
