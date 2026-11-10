using System.ComponentModel.DataAnnotations;

namespace Personal_Health_Record_System.Models
{
    public class personal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "帳號(身分證字號)")]
        public string Account { get; set; }//帳號

        [Required]
        [Display(Name = "密碼")]
        public string Password { get; set; }//密碼

        // 基本資訊
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "出生日期")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "性別")]
        public Gender Gender { get; set; }

        // 聯絡資訊
        [Display(Name = "手機")]
        public string MobilePhone { get; set; }

        [Display(Name = "聯絡地址")]
        public string ContactAddress { get; set; }

        // 緊急聯絡人
        [Display(Name = "緊急聯絡人")]
        public string EmergencyContactName { get; set; }

        [Display(Name = "關係")]
        public string EmergencyContactRelationship { get; set; }

        [Display(Name = "緊急聯絡電話")]
        public string EmergencyContactPhone { get; set; }

        // 健康資訊
        [Display(Name = "身高(cm)")]
        public double Height { get; set; }

        [Display(Name = "體重(kg)")]
        public double Weight { get; set; }

        [Display(Name = "職業")]
        public string Occupation { get; set; }

        [Display(Name = "病史")]
        public string MedicalHistory { get; set; }

        [Display(Name = "家族病史")]
        public string FamilyHistory { get; set; }

        [Display(Name = "現在病史")]
        public string CurrentIllness { get; set; }

        // 生活習慣
        [Display(Name = "睡眠習慣")]
        public SleepPattern SleepPattern { get; set; }

        [Display(Name = "早餐習慣")]
        public BreakfastHabit BreakfastHabit { get; set; }

        [Display(Name = "每週運動時數約")]
        public ExerciseHabit ExerciseHabit { get; set; }

        [Display(Name = "吸菸習慣")]
        public SmokingHabit SmokingHabit { get; set; }

        [Display(Name = "飲酒習慣")]
        public AlcoholHabit AlcoholHabit { get; set; }

        [Display(Name = "嚼檳榔習慣")]
        public BetelNutHabit BetelNutHabit { get; set; }

        [Display(Name = "其他健康需求")]
        public string HealthPromotionNeeds { get; set; }
    }
    public enum Gender
    {
        男,女
    }

    public enum SleepPattern
    {
        每日睡足8小時,不足8小時,時常失眠
    }
    public enum BreakfastHabit
    { 
        每天吃,偶爾,不吃
    }
    public enum ExerciseHabit
    {
        少於三十分鐘,半小時至一小時, 一小時至一小時半,一小時半至兩小時,兩小時以上
    }
    public enum SmokingHabit
    {
        不吸菸,吸菸
    }
    public enum AlcoholHabit
    {
        不喝酒,偶爾喝,時常喝
    }
    public enum BetelNutHabit
    {
        不嚼食檳榔,嚼食檳榔
    }
}
