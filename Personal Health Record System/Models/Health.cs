using System.ComponentModel.DataAnnotations;

namespace Personal_Health_Record_System.Models
{
    public class Health
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "帳號(身分證字號)")]
        public string Account { get; set; }//帳號
        // 血壓
        [Required]
        [Display(Name = "收縮壓(mmHg)")]
        public int SystolicPressure { get; set; }  // 收縮壓
        [Required]
        [Display(Name = "舒張壓(mmHg)")]
        public int DiastolicPressure { get; set; } // 舒張壓

        // 生理指標
        [Required]
        [Display(Name = "血氧 (%)")]
        public int BloodOxygen { get; set; }       // 血氧 (%)
        [Required]
        [Display(Name = "脈搏 (bpm)")]
        public int HeartRate { get; set; }         // 脈搏 (bpm)
        [Required]
        [Display(Name = "體溫 (°C)")]
        public float BodyTemperature { get; set; } // 體溫 (°C)

        // 血糖
        [Required]
        [Display(Name = "空腹血糖值 (mg/dL)")]
        public float FastingBloodSugar { get; set; }      // 空腹血糖值 (mg/dL)
        [Required]
        [Display(Name = "飯後血糖值 (mg/dL)")]
        public float PostMealBloodSugar { get; set; }     // 飯後血糖值 (mg/dL)

        // 睡眠與運動
        [Required]
        [Display(Name = "睡眠時長 (小時)")]
        public float SleepDuration { get; set; }          // 睡眠時長 (小時)
        [Required]
        [Display(Name = "運動與否")]
        public bool IsExercise { get; set; }              // 運動與否 (true = 有運動, false = 無運動)

        // 飲食
        [Required]
        [Display(Name = "早餐")]
        public string Breakfast { get; set; }             // 早餐
        [Required]
        [Display(Name = "午餐")]
        public string Lunch { get; set; }                 // 午餐
        [Required]
        [Display(Name = "晚餐")]
        public string Dinner { get; set; }                // 晚餐

        // 紀錄時間
        [Required]
        [Display(Name = "紀錄日期")]
        [DataType(DataType.DateTime)]
        public DateTime RecordDate { get; set; } = DateTime.Now;
    }
}
