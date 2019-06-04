using Hotel.Helpers;
using Hotel.Models.DataAccessLayer;
using Hotel.Models.EntityLayer;
using Hotel.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hotel.Models.BusinessLogicLayer
{
    class FeaturesBLL
    {
        /* Variables */
        FeaturesDAL featuresDAL;

        /* Properties */
        public ObservableCollection<Feature> FeatureList;

        /* Constructor */
        public FeaturesBLL()
        {
            featuresDAL = new FeaturesDAL();
            FeatureList = featuresDAL.GetFeatures();
            FilterUnsignedFeatures();
        }

        public void FilterUnsignedFeatures()
        {
            ObservableCollection<Feature> result = new ObservableCollection<Feature>();
            foreach (var feature in FeatureList)
            {
                if (feature.IdRoom == null)
                {
                    result.Add(feature);
                }
            }
            FeatureList = result;
        }

        public void Add(object param)
        {
            var tuple = param as Tuple<string, string>;

            string type = tuple.Item1;
            int price = int.Parse(tuple.Item2);

            if (String.IsNullOrEmpty(type))
            {
                return;
            }

            Feature feature = new Feature()
            {
                Type = type,
                Price = price,
                IdRoom = null
            };
            featuresDAL.Add(feature);
        }

        public void Update(object param)
        {
            var tuple = param as Tuple<string, string>;

            string type = tuple.Item1;
            int price = int.Parse(tuple.Item2);

            if (String.IsNullOrEmpty(type))
            {
                return;
            }

            Feature feature = new Feature()
            {
                Id = DataManager.Instance.SelectedFeature.Id,
                Type = type,
                Price = price,
                IdRoom = null
            };
            featuresDAL.Update(feature);
        }

        public void Delete(object param)
        {
            var list = (param as ListView);
            if (list != null)
            {
                var index = list.SelectedIndex;
                featuresDAL.Delete(FeatureList.ElementAt(index).Id);
                FeatureList.RemoveAt(index);
            }
        }
    }
}
