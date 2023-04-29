namespace SuperSold.Data.EfCoreDB;

public abstract class EfCoreRepository {

    protected readonly EfCoreDBContext Context;
    public EfCoreRepository(EfCoreDBContext context) {
        Context = context;
    }

}
