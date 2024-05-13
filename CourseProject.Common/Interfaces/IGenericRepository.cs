using System.Linq.Expressions;
using CourseProject.Common.Models;

namespace CourseProject.Common.Interfaces;

public interface IGenericRepository<T> where T  : BaseEntity
{
    // ใช้ในการเก็บและจัดการกับโค้ดในรูปแบบข้อมูลที่สามารถตรวจสอบหรือแปลงได้ ซึ่งมีประโยชน์ในการสร้างและจัดการกับการดำเนินการที่ซับซ้อน โดยเฉพาะในการสร้างคิวรี LINQ หรือการสร้างคิวรี SQL แบบไดนามิก
    // ในกรณีของ GetFilteredAsync(Expression<Func<T , bool>>[] filters), มันคือเมธอดที่รับอาร์เรย์ของฟังก์ชันที่รับพารามิเตอร์เป็น T (ที่เป็น BaseEntity หรือ subclass ของมัน) และคืนค่าเป็น boolean. ฟังก์ชันเหล่านี้จะถูกใช้เป็นตัวกรองในการดึงข้อมูลจากฐานข้อมูล.
    // คำสั่ง params ใช้สำหรับระบุว่าพารามิเตอร์สุดท้ายในเมธอดสามารถรับค่าเข้ามาเป็นจำนวนที่ไม่จำกัด โดยจะถูกจัดเก็บในอาร์เรย์
    Task<List<T>> GetFilteredAsync(Expression<Func<T , bool>>[] filters , int? skip , int? take , params Expression<Func<T , object>>[] includes);
    Task<List<T>> GetAsync(int? skip , int? take , params Expression<Func<T , object>>[] includes);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<int> InsertAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}