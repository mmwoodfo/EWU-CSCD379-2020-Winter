using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;


namespace SecretSanta.Web.Api
{
    [ModelMetadataType(typeof(UserInputMetadata))]
    public partial class UserInput
    {
    }
    public class UserInputMetadata
    {
        //Justification: Metadata - it should be fine that fields are uninitialized
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    }
}


