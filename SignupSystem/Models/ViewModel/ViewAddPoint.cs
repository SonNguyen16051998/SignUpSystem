using System.Collections.Generic;

namespace SignupSystem.Models.ViewModel
{
    public class ViewAddPoint
    {
        public string Code_Subject { get; set; }
        public int Id_PointType { get; set; }
        public List<ViewAddPointStudent> PointStudent { get; set; }
    }
}
