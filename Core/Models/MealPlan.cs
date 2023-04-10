using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;

namespace BlitzerCore.Models
{
    [NotMapped]
    public class MealPlan
    {
        public enum Type { ALL_INCLUSIVE, A_LA_CARTE, BREAKFEAST_ONLY}

        public const string ALL_INCLUSIVE = "All Inclusive";
        public const string A_LA_CARTE = "A la carte";

        public string Name { get; set; }
        public Type PlanType { get; set; }
        public string Value
        {
            set {
                if (value.ToUpper() == "AI" || value == ALL_INCLUSIVE)
                {
                    Name = ALL_INCLUSIVE;
                    PlanType = Type.ALL_INCLUSIVE;
                }
                else if (value.ToUpper() == "EP" || value == A_LA_CARTE)
                {
                    Name = A_LA_CARTE;
                    PlanType = Type.A_LA_CARTE;
                }
                else
                {
                    Name = "Standard";
                    PlanType = Type.BREAKFEAST_ONLY;
                }
            }
            get { return Name; }
        }

        public MealPlan() {
            Name = "Standard";
        }

        public MealPlan (string aInput )
        {
            Value = aInput;
        }

        public MealPlan(Staging.Hotel aResort)
        {
            if (aResort.Amenities != null && aResort.Amenities.Exists(x => x.Amenity.Type == ALL_INCLUSIVE))
                Value = ALL_INCLUSIVE;
            else
                Value = A_LA_CARTE;
        }
    }
}
