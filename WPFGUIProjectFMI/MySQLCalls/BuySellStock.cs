using System;
using System.Reflection;
using System.Windows;

namespace WPFGUIProjectFMI
{
    public static class BuySellStock
    {
        #region Methods for buying or selling stock

        /// <summary>
        ///Array of property names
        /// </summary>
        static string[] propertyName = new string[12] { "Tomatoes", "Potatoes", "Aples", "Bananas", "Oranges", "Strawberries", "Beans", "Kiwi", "Pears", "Cucumbers", "Watermelons", "Cantaloupes" };
        static Type myType = typeof(CombinedViewModel);

        /// <summary>
        /// Method that buys stock if enough money and storage
        /// </summary>
        public static void BuyStock()
        {
            float shopValue = 0;
            int itemCount = 0;
            foreach (string propertyName in propertyName)
            {
                PropertyInfo propertyPrice = myType.GetProperty("price" + propertyName);
                PropertyInfo propertyCount = myType.GetProperty("shop" + propertyName);
                PropertyInfo setProperty = myType.GetProperty(propertyName);
                setProperty.SetValue(setProperty, int.Parse(setProperty.GetValue(setProperty).ToString()) + int.Parse(propertyCount.GetValue(propertyCount).ToString()));
                shopValue += float.Parse(propertyPrice.GetValue(propertyCount).ToString()) * int.Parse(propertyCount.GetValue(propertyCount).ToString());
                itemCount += int.Parse(propertyCount.GetValue(propertyCount).ToString());
            }
            CombinedViewModel.Money -= shopValue;
            CombinedViewModel.CurrentStorage -= itemCount;
        }

        /// <summary>
        /// Method that sells stock if enough storage and items in storage
        /// </summary>
        public static void SellStock()
        {
            float shopValue = 0;
            int itemCount = 0;
            bool unSoldItems = false;
            foreach (string propertyName in propertyName)
            {
                PropertyInfo propertyPrice = myType.GetProperty("price" + propertyName);
                PropertyInfo propertyCount = myType.GetProperty("shop" + propertyName);
                PropertyInfo setProperty = myType.GetProperty(propertyName);
                if (int.Parse(setProperty.GetValue(setProperty).ToString()) >= int.Parse(propertyCount.GetValue(propertyCount).ToString()))
                {
                    setProperty.SetValue(setProperty, int.Parse(setProperty.GetValue(setProperty).ToString()) - int.Parse(propertyCount.GetValue(propertyCount).ToString()));
                    shopValue += float.Parse(propertyPrice.GetValue(propertyCount).ToString()) * int.Parse(propertyCount.GetValue(propertyCount).ToString());
                    itemCount += int.Parse(propertyCount.GetValue(propertyCount).ToString());
                }
                else
                {
                    unSoldItems = true;
                }
            }
            if (unSoldItems)
            {
                ShowMessageBox.DefineError("UnsoldItems");
            }
            CombinedViewModel.Money += shopValue;
            CombinedViewModel.CurrentStorage += itemCount;
        }

        #endregion
    }
}
