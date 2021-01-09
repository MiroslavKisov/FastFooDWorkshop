namespace FastFoodWorkshop.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        public int FastFoodUserId { get; set; }
        public virtual FastFoodUser FastFoodUser { get; set; }

        public string RecipeDescription { get; set; }
    }
}