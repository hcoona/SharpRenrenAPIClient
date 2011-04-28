using System.Collections.Generic;
using Newtonsoft.Json;

namespace Renren.API.Entity
{
    public class UserInfoEntity
    {
        [JsonProperty(PropertyName = "uid")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "birthday")]
        public string Birthday { get; set; }
        [JsonProperty(PropertyName = "tinyurl")]
        public string TinyHeadImageUrl { get; set; }
        [JsonProperty(PropertyName = "sex")]
        public SexEnum Sex { get; set; }
        [JsonProperty(PropertyName = "university_history")]
        public List<UniversityInfo> UniversityHistory { get; set; }
        [JsonProperty(PropertyName = "work_history")]
        public List<WorkInfo> WorkHistory { get; set; }
        [JsonProperty(PropertyName = "star")]
        public bool IsStarUser { get; set; }
        [JsonProperty(PropertyName = "mainurl")]
        public string MainHeadImageUrl { get; set; }
        [JsonProperty(PropertyName = "vip")]
        public bool IsVipUser { get; set; }
        [JsonProperty(PropertyName = "hometown_location")]
        public HometownLocationWrapper HometownLocation { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "email_hash")]
        public string EmailHash { get; set; }
        [JsonProperty(PropertyName = "zidou")]
        public bool IsZiDouUser { get; set; }
        [JsonProperty(PropertyName = "hs_history")]
        public List<HighSchoolInfo> HighSchoolHistory { get; set; }

        public enum SexEnum
        {
            Female = 0,
            Male = 1
        }

        public class UniversityInfo
        {
            [JsonProperty(PropertyName = "department")]
            public string Department { get; set; }
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "year")]
            public int Year { get; set; }
        }

        public class WorkInfo
        {
            [JsonProperty(PropertyName = "company_name")]
            public string CompanyName { get; set; }
            [JsonProperty(PropertyName = "description")]
            public string Description { get; set; }
            [JsonProperty(PropertyName = "start_date")]
            public string StartDate { get; set; }
            [JsonProperty(PropertyName = "end_date")]
            public string EndDate { get; set; }
        }

        public class HometownLocationWrapper
        {
            [JsonProperty(PropertyName = "province")]
            public string Province { get; set; }
            [JsonProperty(PropertyName = "city")]
            public string City { get; set; }
        }

        public class HighSchoolInfo
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "grad_year")]
            public int GradeYear { get; set; }
        }
    }
}