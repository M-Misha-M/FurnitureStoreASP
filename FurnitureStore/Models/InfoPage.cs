using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureStore.Models
{


    //Modeling View with pages
    public class InfoPage
    {
        //Count of goodies
        public int TotalItems { get; set; }

        //Count of goodies in current page
        public int ItemsPerPage { get; set; }

        //Current page number
       public int CurrentPage { get; set; }

        //Total pages
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
                
            
    }
}