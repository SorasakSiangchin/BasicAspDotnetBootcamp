using System.Linq.Expressions;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Infrastructure;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    // DbSet คือคลาสที่ให้คุณสามารถทำงานกับฐานข้อมูลโดยใช้ LINQ และเป็นที่เก็บข้อมูลที่เกี่ยวข้องกับตารางในฐานข้อมูล
    private readonly DbSet<T> DbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
    }
    
    public async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;
        
        foreach (var filter in filters) query = query.Where(filter);
        
        foreach (var include in includes) query = query.Include(include);

        if (skip != null) query = query.Skip(skip.Value);

        if (take != null) query = query.Take(take.Value);

        return await query.ToListAsync();
    }

    public async Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;

        foreach (var include in includes) query = query.Include(include);
        
        if(skip != null) query = query.Skip(skip.Value);
        
        if(take != null) query = query.Take(take.Value);

        return await query.ToListAsync();

    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;
        
        query = query.Where(entity => entity.Id == id);
        
        foreach (var include in includes) query = query.Include(include);

        return await query.SingleOrDefaultAsync();

    }

    public async Task<int> InsertAsync(T entity)
    {
        await DbSet.AddAsync(entity);

        return entity.Id;
    }

    public void Update(T entity)
    {
        // DbSet.Attach(entity); คือการแนบ entity ที่ระบุไว้กับ DbContext ที่เกี่ยวข้อง ทำให้ DbContext เริ่มติดตามการเปลี่ยนแปลงที่เกิดขึ้นกับ entity นั้น
        DbSet.Attach(entity);
        
        // EntityState.Modified คือสถานะที่ระบุว่า entity นั้นมีการเปลี่ยนแปลงและต้องการที่จะอัปเดตค่าในฐานข้อมูล
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        // ส่วนของโค้ดนี้ตรวจสอบว่า entity ที่ต้องการลบมีสถานะเป็น Detached หรือไม่ ถ้าเป็น Detached หมายความว่า entity นี้ไม่ถูกติดตามโดย DbContext ดังนั้นเราจึงต้องทำการ Attach entity กลับเข้าไปใน DbContext ก่อนที่จะทำการลบ
        if(_context.Entry(entity).State == EntityState.Detached)
        {
            DbSet.Attach(entity);
        }
        
        DbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}