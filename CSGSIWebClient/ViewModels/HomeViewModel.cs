using CSGSIWebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSGSIWebClient.ViewModels
{
    public class HomeViewModel
    {
        public string Title { get; set; }
        public List<CSMatch> Matches { get; set; }
    }
}
