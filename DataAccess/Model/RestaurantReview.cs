//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RestaurantReview
    {
        public int RestaurantReviewId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string Review { get; set; }
        public int UserProfileId { get; set; }
        public int RestaurantId { get; set; }
    
        public virtual Restaurant Restaurant { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
