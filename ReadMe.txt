---Model Projesi Adımları---


References=>EntityFramework.

NOT**:Eğer Enum ile Statüler gösterilmeyecek ise Enum klasörü tamamen atlanabilir.Base içerisine property eklenmemeli.

1)Solution içerisine C# Library Eklenir.
2)Context,Entity,Enum,Map klasörleri açılır.
3)Entity Klasörü içerisine BaseEntity class'ı açılır.
3.1)**Enum Klasörü içerisine Statu açılarak Base içerisine property eklenir.
3.1)Bu sınıf diğer entity sınıflarımızın ortak propertylerini içerir.
4)Diğer Entity sınıfları açılır.(Category ve Product)
5)Map sınıfı içerisine öncelikle BaseMap içerisindeki property'ler için map işlemi yapılır.
5.1)CategoryMap ve ProductMap BaseMap'ten miras alacaktır. Aralarındaki ilişki(One to Many) mapleme ile gösterilecektir.
Not:CategoryMap içerisindeki ilişkiyi gösteren property gerekli değildir. Diğer taraftan gösterimi için eklenmiştir.
6)Context Klasörü içerisine ProjectContext açılır. OnModelCreating metodu override edilerek yazmış olduğumuz map sınıfları ayarlara eklenir.
6.1)DbSetler açılır.
7)enable-migrations veya -enableAutomaticMigrations ile console üzerinden migrate ediyoruz. Update-database yapıyoruz.
Model projesini forms içeren projeye referans vermeyi unutmayınız.


