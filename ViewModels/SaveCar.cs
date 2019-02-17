namespace Evoflare.API.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Evoflare.API.ViewModelSchemaFilters;
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// A make and model of car.
    /// </summary>
    [SwaggerSchemaFilter(typeof(SaveCarSchemaFilter))]
    public class SaveCar
    {
        /// <summary>
        /// The number of cylinders in the cars engine.
        /// </summary>
        [Range(1, 20)]
        public int Cylinders { get; set; }

        /// <summary>
        /// The make of the car.
        /// </summary>
        [Required]
        public string Make { get; set; }

        /// <summary>
        /// The model of the car.
        /// </summary>
        [Required]
        public string Model { get; set; }
    }
}
