using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        

        public void AddItem(Furniture furniture, int quantity)
        {
            CartLine line = lineCollection.Where(g => g.Furniture.FurnitureId == furniture.FurnitureId).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Furniture = furniture,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity = quantity;
            }

           
        }



        public void RemoveLine(Furniture furniture)
        {
            lineCollection.RemoveAll(l => l.Furniture.FurnitureId ==furniture.FurnitureId);
        }


        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Furniture.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }


    public class CartLine
    {
       
        public Furniture Furniture { get; set; }
        public int Quantity { get; set; }
    }
}