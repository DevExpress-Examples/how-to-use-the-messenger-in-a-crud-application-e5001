using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataAnnotationAttributes.Model {
    public enum Gender { Female = 0, Male = 1 }
    public class GenderList : ReadOnlyCollection<Gender> {
        public static IList<Gender> GetGenderValues() {
            IList res = Enum.GetValues(typeof(Gender));
            res = (IList)res.Cast<Gender>();
            return (IList<Gender>)res;
        }
        public GenderList()
            : base(GetGenderValues()) {
        }
    }
}