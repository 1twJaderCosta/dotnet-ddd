namespace MercGlobalMain.Infra.Repositories {
    public class UserRepository : BaseRepository<User>, IUserRepository {
        private readonly MercGlobalMainContext _context;

        public UserRepository(MercGlobalMainContext context) {
            _context = context;
        }

        public virtual async Task<T> Create(T obj) {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task<T> Update(T obj) {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task Remove(long id) {
            var obj = await Get(id);

            if(obj != null) {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<T> Get(long id) {
            var obj = await _context.Set(T)()
                                    .AsNoTracking()
                                    .Where(x=>x.Id == id)
                                    .ToListAsync();
            return obj.FirstOrDefault();
        }

        public virtual async Task<List<T>> Get() {
            return await _context.Set<T>()
                                .AsNoTracking()
                                .ToListAsync();
        }
    }
    }    
}