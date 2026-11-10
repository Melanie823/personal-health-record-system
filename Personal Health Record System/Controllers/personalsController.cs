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
    public class personalsController : Controller
    {
        private readonly Personal_Health_Record_SystemContext _context;

        public personalsController(Personal_Health_Record_SystemContext context)
        {
            _context = context;
        }

        // GET: personals
        public async Task<IActionResult> Index()
        {
            return View(await _context.personal.ToListAsync());
        }

        // GET: personals/Details/5
        public async Task<IActionResult> Details(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                return NotFound();
            }

            var personal = await _context.personal
                                         .FirstOrDefaultAsync(p => p.Account == account);

            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        // GET: personals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: personals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Account,Password,Name,DateOfBirth,Gender,MobilePhone,ContactAddress,EmergencyContactName,EmergencyContactRelationship,EmergencyContactPhone,Height,Weight,Occupation,MedicalHistory,FamilyHistory,CurrentIllness,SleepPattern,BreakfastHabit,ExerciseHabit,SmokingHabit,AlcoholHabit,BetelNutHabit,HealthPromotionNeeds")] personal personal)
        {
            if (ModelState.IsValid)
            {
                // 檢查帳號是否已存在
                var existingAccount = await _context.personal
                    .FirstOrDefaultAsync(p => p.Account == personal.Account);

                if (existingAccount != null)
                {
                    // 帳號已存在，加入 ModelState 錯誤
                    ModelState.AddModelError("Account", "該帳號已經被註冊");
                    return View(personal);
                }

                _context.Add(personal);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(personal);
        }

        // AJAX 檢查帳號是否重複 (即時檢查)
        [HttpGet]
        public async Task<JsonResult> CheckAccountExists(string account)
        {
            var exists = await _context.personal
                .AnyAsync(p => p.Account == account);
            return Json(!exists);  // true 表示帳號可用，false 表示帳號已存在
        }

        // GET: personals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personal = await _context.personal.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            return View(personal);
        }

        // POST: personals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Account,Password,Name,DateOfBirth,Gender,MobilePhone,ContactAddress,EmergencyContactName,EmergencyContactRelationship,EmergencyContactPhone,Height,Weight,Occupation,MedicalHistory,FamilyHistory,CurrentIllness,SleepPattern,BreakfastHabit,ExerciseHabit,SmokingHabit,AlcoholHabit,BetelNutHabit,HealthPromotionNeeds")] personal personal)
        {
            if (id != personal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!personalExists(personal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Login", "Home");
            }
            return View(personal);
        }

        // GET: personals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personal = await _context.personal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        // POST: personals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personal = await _context.personal.FindAsync(id);
            if (personal != null)
            {
                _context.personal.Remove(personal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool personalExists(int id)
        {
            return _context.personal.Any(e => e.Id == id);
        }
    }
}
