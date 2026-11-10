using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Personal_Health_Record_System.Data;
using Personal_Health_Record_System.Models;

namespace Personal_Health_Record_System.Controllers
{
    public class HealthsController : Controller
    {
        private readonly Personal_Health_Record_SystemContext _context;

        public HealthsController(Personal_Health_Record_SystemContext context)
        {
            _context = context;
        }

        // GET: Healths
        public async Task<IActionResult> Index()
        {
            // 從 Session 取得登入帳號
            var account = HttpContext.Session.GetString("UserAccount");

            // 只顯示符合當前登入帳號的健康紀錄
            var healthRecords = await _context.Health
                .Where(h => h.Account == account)
                .ToListAsync();

            return View(healthRecords);
        }

        public async Task<IActionResult> Filter(DateTime? startDate, DateTime? endDate, bool? isExercise)
        {
            var account = HttpContext.Session.GetString("UserAccount");
            var healthRecords = _context.Health.Where(h => h.Account == account);

            if (startDate.HasValue)
            {
                healthRecords = healthRecords.Where(h => h.RecordDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                healthRecords = healthRecords.Where(h => h.RecordDate <= endDate.Value);
            }
            if (isExercise.HasValue)
            {
                healthRecords = healthRecords.Where(h => h.IsExercise == isExercise.Value);
            }

            var filteredRecords = await healthRecords.ToListAsync();
            return View("Index", filteredRecords);
        }

        // GET: Healths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var health = await _context.Health
                .FirstOrDefaultAsync(m => m.Id == id);
            if (health == null)
            {
                return NotFound();
            }

            return View(health);
        }

        // 新增分析頁面
        public async Task<IActionResult> Analyze(int id)
        {
            var health = await _context.Health.FindAsync(id);
            if (health == null)
            {
                return NotFound();
            }

            var analysis = new List<string>();

            if (health.SystolicPressure > 120 || health.DiastolicPressure > 80)
            {
                analysis.Add("血壓偏高，請注意保持放鬆與健康飲食。");
            }
            if (health.SystolicPressure < 90 || health.DiastolicPressure < 60)
            {
                analysis.Add("血壓偏低，請注意避免長時間站立，保持適當休息。");
            }
            if (health.BloodOxygen < 95)
            {
                analysis.Add("血氧過低，請注意保持室內通風，必要時深呼吸。");
            }
            if (health.HeartRate < 60 || health.HeartRate > 100)
            {
                analysis.Add("脈搏異常，請注意多休息，避免過度勞累。");
            }
            if (health.BodyTemperature > 37.5)
            {
                analysis.Add("體溫過高，請注意身體健康歐!");
            }
            if (health.FastingBloodSugar < 70 || health.FastingBloodSugar > 99)
            {
                analysis.Add("空腹血糖異常，請注意飲食均衡，保持適量運動。");
            }
            if (health.PostMealBloodSugar < 70 || health.PostMealBloodSugar > 140)
            {
                analysis.Add("飯後血糖異常，避免過量進食甜食。");
            }
            if (health.SleepDuration < 7 || health.SleepDuration > 8)
            {
                analysis.Add("睡眠時間異常，請注意早睡早起，保持充足睡眠。");
            }
            if (!health.IsExercise)
            {
                analysis.Add("您今天還沒有運動喔，記得適度運動保持健康！");
            }
            if (health.Breakfast == "無" || health.Lunch == "無" || health.Dinner == "無")
            {
                analysis.Add("三餐不完整，記得按時進食，保持營養均衡。");
            }            
            if (!analysis.Any())
            {
                analysis.Add("恭喜您！您的健康狀況一切正常，請繼續保持良好的生活習慣，健康生活每一天！");
            }

            ViewBag.Analysis = analysis;
            return View(health);
        }

        // GET: Healths/Create
        public IActionResult Create()
        {
            // 從 Session 取得登入帳號
            var account = HttpContext.Session.GetString("UserAccount");

            // 將帳號填入 ViewBag，傳到前端畫面
            ViewBag.Account = account;

            return View();
        }

        // POST: Healths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Account,SystolicPressure,DiastolicPressure,BloodOxygen,HeartRate,BodyTemperature,FastingBloodSugar,PostMealBloodSugar,SleepDuration,IsExercise,Breakfast,Lunch,Dinner,RecordDate")] Health health)
        {
            // 從 Session 取得登入帳號，並自動填入 health 物件
            health.Account = HttpContext.Session.GetString("UserAccount");

            if (ModelState.IsValid)
            {
                _context.Add(health);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(health);
        }

        // GET: Healths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var health = await _context.Health.FindAsync(id);
            if (health == null)
            {
                return NotFound();
            }
            return View(health);
        }

        // POST: Healths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Account,SystolicPressure,DiastolicPressure,BloodOxygen,HeartRate,BodyTemperature,FastingBloodSugar,PostMealBloodSugar,SleepDuration,IsExercise,Breakfast,Lunch,Dinner,RecordDate")] Health health)
        {
            if (id != health.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(health);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthExists(health.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(health);
        }

        // GET: Healths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var health = await _context.Health
                .FirstOrDefaultAsync(m => m.Id == id);
            if (health == null)
            {
                return NotFound();
            }

            return View(health);
        }

        // POST: Healths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var health = await _context.Health.FindAsync(id);
            if (health != null)
            {
                _context.Health.Remove(health);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HealthExists(int id)
        {
            return _context.Health.Any(e => e.Id == id);
        }
    }
}
