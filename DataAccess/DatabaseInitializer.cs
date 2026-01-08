using System.Data.SQLite;
using VibeMap.Utils;
using System;

namespace VibeMap.DataAccess
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            InitializeUsers();
            InitializeCatalog();
        }

        private static void InitializeUsers()
        {
            try
            {
                using (var connection = DbConnection.GetUserConnection())
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand(connection))
                    {
                        cmd.CommandText = @"
                          CREATE TABLE IF NOT EXISTS Users (
                            UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL UNIQUE,
                            PasswordHash TEXT NOT NULL,
                            CreatedAt TEXT NOT NULL,
                            ProfilePicturePath TEXT,
                            DisplayName TEXT
                          );
                          CREATE TABLE IF NOT EXISTS UserActions (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL,
                            Category TEXT NOT NULL,
                            Title TEXT NOT NULL,
                            Status TEXT NOT NULL,
                            Timestamp TEXT NOT NULL
                          );";
                        cmd.ExecuteNonQuery();

                        TryAddColumn(cmd, "Users", "ProfilePicturePath", "TEXT");
                        TryAddColumn(cmd, "Users", "DisplayName", "TEXT");
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Kullanıcı Veritabanı Hazırlama Hatası: " + ex.Message, ex); }
        }

        private static void InitializeCatalog()
        {
            try
            {
                using (var connection = DbConnection.GetCatalogConnection())
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand(connection))
                    {
                        cmd.CommandText = @"
                          CREATE TABLE IF NOT EXISTS Recommendations (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Category TEXT NOT NULL,
                            Mood TEXT NOT NULL,
                            Title TEXT NOT NULL UNIQUE,
                            Description TEXT,
                            ImagePath TEXT,
                            ActionLink TEXT
                          );";
                        cmd.ExecuteNonQuery();

                        SeedRecommendations(connection);
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Katalog Veritabanı Hazırlama Hatası: " + ex.Message, ex); }
        }

        private static void TryAddColumn(SQLiteCommand cmd, string table, string column, string type)
        {
            try
            {
                cmd.CommandText = $"ALTER TABLE {table} ADD COLUMN {column} {type};";
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException) { /* Column likely already exists */ }
        }

        private static void SeedRecommendations(SQLiteConnection con)
        {
            // Force re-seed if any English categories exist (e.g., "GAMES")
            bool needsReseed = false;
            using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Recommendations WHERE Category = 'GAMES'", con))
            {
                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0) needsReseed = true;
            }

            if (!needsReseed)
            {
                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Recommendations", con))
                {
                    if (Convert.ToInt32(cmd.ExecuteScalar()) >= 300) return;
                }
            }

            using (var transaction = con.BeginTransaction())
            {
                try
                {
                    using (var cmdDel = new SQLiteCommand("DELETE FROM Recommendations", con)) cmdDel.ExecuteNonQuery();

                    string sql = "INSERT OR IGNORE INTO Recommendations (Category, Mood, Title, Description, ImagePath, ActionLink) VALUES (@c, @m, @t, @d, @i, @a)";

                    // --- OYUNLAR (50+ Gerçek Veri) ---
                    AddBatch(con, sql, "OYUNLAR", new[] {
                        ("Enerjik", "Black Myth: Wukong", "Çin mitolojisine dayanan bir aksiyon RPG oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/2358720/header.jpg", "https://store.steampowered.com/app/2358720/"),
                        ("Enerjik", "Helldivers 2", "Düşman bir galakside takım tabanlı nişancı oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/553850/header.jpg", "https://store.steampowered.com/app/553850/"),
                        ("Mutlu", "Palworld", "Gizemli yaratıklarla arkadaş olun ve onları toplayın.", "https://cdn.akamai.steamstatic.com/steam/apps/1623730/header.jpg", "https://store.steampowered.com/app/1623730/"),
                        ("Mutlu", "Manor Lords", "Şehir kurma özelliğine sahip ortaçağ strateji oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/1363080/header.jpg", "https://store.steampowered.com/app/1363080/"),
                        ("Enerjik", "Path of Exile 2", "Yeni nesil aksiyon RPG oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/2728280/header.jpg", "https://store.steampowered.com/app/2728280/"),
                        ("Enerjik", "Space Marine 2", "Galaksi tehlikede. Dünyalar düşüyor.", "https://cdn.akamai.steamstatic.com/steam/apps/2183900/header.jpg", "https://store.steampowered.com/app/2183900/"),
                        ("Gergin", "Elden Ring", "Yüksel, Kararmış ve zarafetin rehberliğinde ilerle.", "https://cdn.akamai.steamstatic.com/steam/apps/1245620/header.jpg", "https://store.steampowered.com/app/1245620/"),
                        ("Mutlu", "Balatro", "Pokerden ilham alan bir roguelike deste oluşturma oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/2567290/header.jpg", "https://store.steampowered.com/app/2567290/"),
                        ("Enerjik", "Hades II", "Yeraltı dünyasının ötesinde savaşın.", "https://cdn.akamai.steamstatic.com/steam/apps/1145350/header.jpg", "https://store.steampowered.com/app/1145350/"),
                        ("Gergin", "Baldur's Gate 3", "Partinizi toplayın; Unutulmuş Diyarlar'a geri dönün.", "https://cdn.akamai.steamstatic.com/steam/apps/1086940/header.jpg", "https://store.steampowered.com/app/1086940/"),
                        ("Mutlu", "Stardew Valley", "Kırsala taşının ve yeni bir hayata başlayın.", "https://cdn.akamai.steamstatic.com/steam/apps/413150/header.jpg", "https://store.steampowered.com/app/413150/"),
                        ("Mutlu", "Dave the Diver", "Gündüz derin denizi keşfedin, gece bir suşi barı işletin.", "https://cdn.akamai.steamstatic.com/steam/apps/1868140/header.jpg", "https://store.steampowered.com/app/1868140/"),
                        ("Enerjik", "Cyberpunk 2077", "Night City'de geçen bir açık dünya hikayesi.", "https://cdn.akamai.steamstatic.com/steam/apps/1091500/header.jpg", "https://store.steampowered.com/app/1091500/"),
                        ("Enerjik", "Apex Legends", "Bu kahraman nişancı oyununda karakterinizle fethedin.", "https://cdn.akamai.steamstatic.com/steam/apps/1172470/header.jpg", "https://store.steampowered.com/app/1172470/"),
                        ("Enerjik", "Destiny 2", "Gizemleri keşfetmek için Destiny 2 dünyasına dalın.", "https://cdn.akamai.steamstatic.com/steam/apps/1085660/header.jpg", "https://store.steampowered.com/app/1085660/"),
                        ("Enerjik", "Counter-Strike 2", "Dünyanın önde gelen taktiksel nişancı oyununun yeni bölümü.", "https://cdn.akamai.steamstatic.com/steam/apps/730/header.jpg", "https://store.steampowered.com/app/730/"),
                        ("Enerjik", "Black Ops 6", "Sinematik bir tek oyunculu hikaye modunda ikonik Black Ops aksiyonu.", "https://cdn.akamai.steamstatic.com/steam/apps/2146340/header.jpg", "https://store.steampowered.com/app/2146340/"),
                        ("Mutlu", "Dota 2", "Her gün, dünya çapında milyonlarca oyuncu savaşa giriyor.", "https://cdn.akamai.steamstatic.com/steam/apps/570/header.jpg", "https://store.steampowered.com/app/570/"),
                        ("Gergin", "PUBG: Battlegrounds", "İniş yapın, yağmalayın ve rakiplerinizi zekice alt edin.", "https://cdn.akamai.steamstatic.com/steam/apps/578080/header.jpg", "https://store.steampowered.com/app/578080/"),
                        ("Enerjik", "Warframe", "Durdurulamaz bir savaşçı olarak uyanın ve arkadaşlarınızla birlikte savaşın.", "https://cdn.akamai.steamstatic.com/steam/apps/230410/header.jpg", "https://store.steampowered.com/app/230410/"),
                        ("Enerjik", "War Thunder", "En kapsamlı oynaması ücretsiz MMO askeri oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/236390/header.jpg", "https://store.steampowered.com/app/236390/"),
                        ("Enerjik", "The First Descendant", "Oynaması ücretsiz üçüncü şahıs kooperatif aksiyon RPG nişancı oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/2074920/header.jpg", "https://store.steampowered.com/app/2074920/"),
                        ("Enerjik", "Sparking Zero", "Budokai Tenkaichi serisinin efsanevi oynanışı.", "https://cdn.akamai.steamstatic.com/steam/apps/1790600/header.jpg", "https://store.steampowered.com/app/1790600/"),
                        ("Mutlu", "EA Sports FC 25", "EA SPORTS FC™ 25 size kulüple kazanmanız için daha fazla yol sunuyor.", "https://cdn.akamai.steamstatic.com/steam/apps/2669320/header.jpg", "https://store.steampowered.com/app/2669320/"),
                        ("Gergin", "Dead by Daylight", "Bir oyuncunun Katil rolünü üstlendiği çok oyunculu bir korku oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/381210/header.jpg", "https://store.steampowered.com/app/381210/"),
                        ("Gergin", "Throne and Liberty", "Oynaması ücretsiz, çok platformlu bir MMORPG.", "https://cdn.akamai.steamstatic.com/steam/apps/1985790/header.jpg", "https://store.steampowered.com/app/1985790/"),
                        ("Mutlu", "Enshrouded", "Siz Flameborn'sunuz, can çekişen bir ırkın son umut ışığısınız.", "https://cdn.akamai.steamstatic.com/steam/apps/1203620/header.jpg", "https://store.steampowered.com/app/1203620/"),
                        ("Enerjik", "Naraka: Bladepoint", "60 oyuncuya kadar aksiyonlu bir dövüş deneyimi.", "https://cdn.akamai.steamstatic.com/steam/apps/1203220/header.jpg", "https://store.steampowered.com/app/1203220/"),
                        ("Gergin", "Gray Zone Warfare", "Taktiksel bir birinci şahıs nişancı oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/2479520/header.jpg", "https://store.steampowered.com/app/2479520/"),
                        ("Gergin", "Rainbow Six Siege", "Yıkım ve teknolojik aletlerin sanatında ustalaşın.", "https://cdn.akamai.steamstatic.com/steam/apps/359550/header.jpg", "https://store.steampowered.com/app/359550/"),
                        ("Mutlu", "Granblue Fantasy: Relink", "Göklerde büyük bir macera sizi bekliyor.", "https://cdn.akamai.steamstatic.com/steam/apps/881020/header.jpg", "https://store.steampowered.com/app/881020/"),
                        ("Enerjik", "Ghost of Tsushima", "13. yüzyılın sonlarında Moğol imparatorluğu ulusları kırıp geçirdi.", "https://cdn.akamai.steamstatic.com/steam/apps/2215430/header.jpg", "https://store.steampowered.com/app/2215430/"),
                        ("Mutlu", "Farming Simulator 25", "Şimdiye kadarki en kapsamlı çiftçilik simulasyonu.", "https://cdn.akamai.steamstatic.com/steam/apps/2300320/header.jpg", "https://store.steampowered.com/app/2300320/"),
                        ("Enerjik", "Marvel Rivals", "Süper Kahraman Takım Tabanlı PVP Nişancı Oyunu!", "https://cdn.akamai.steamstatic.com/steam/apps/2767030/header.jpg", "https://store.steampowered.com/app/2767030/"),
                        ("Enerjik", "Tekken 8", "En uzun süredir devam eden video oyunu hikayesinin bir sonraki bölümüne girin.", "https://cdn.akamai.steamstatic.com/steam/apps/1778820/header.jpg", "https://store.steampowered.com/app/1778820/"),
                        ("Gergin", "Dragon Age: The Veilguard", "Veilguard'ı toplayın ve tanrılara meydan okuyun.", "https://cdn.akamai.steamstatic.com/steam/apps/1845910/header.jpg", "https://store.steampowered.com/app/1845910/"),
                        ("Mutlu", "Last Epoch", "Geçmişi ortaya çıkarın, geleceği yeniden kurun.", "https://cdn.akamai.steamstatic.com/steam/apps/899770/header.jpg", "https://store.steampowered.com/app/899770/"),
                        ("Mutlu", "Satisfactory", "Birinci şahıs açık dünya fabrika kurma oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/526870/header.jpg", "https://store.steampowered.com/app/526870/"),
                        ("Mutlu", "Portal 2", "Yenilikçi bulmaca-macera oyunu devam ediyor.", "https://cdn.akamai.steamstatic.com/steam/apps/620/header.jpg", "https://store.steampowered.com/app/620/"),
                        ("Mutlu", "Terraria", "Kazın, savaşın, keşfedin, inşa edin!", "https://cdn.akamai.steamstatic.com/steam/apps/105600/header.jpg", "https://store.steampowered.com/app/105600/"),
                        ("Enerjik", "Left 4 Dead 2", "Kooperatif aksiyon korku FPS oyunu.", "https://cdn.akamai.steamstatic.com/steam/apps/550/header.jpg", "https://store.steampowered.com/app/550/"),
                        ("Gergin", "The Witcher 3: Wild Hunt", "Profesyonel bir canavar avcısı olun.", "https://cdn.akamai.steamstatic.com/steam/apps/292030/header.jpg", "https://store.steampowered.com/app/292030/"),
                        ("Mutlu", "Cities: Skylines", "Simülasyona modern bir yaklaşım.", "https://cdn.akamai.steamstatic.com/steam/apps/255710/header.jpg", "https://store.steampowered.com/app/255710/"),
                        ("Gergin", "RimWorld", "Bir bilim kurgu koloni simülasyonu.", "https://cdn.akamai.steamstatic.com/steam/apps/294100/header.jpg", "https://store.steampowered.com/app/294100/"),
                        ("Mutlu", "Factorio", "Fabrikalar kurun ve bakımını yapın.", "https://cdn.akamai.steamstatic.com/steam/apps/427520/header.jpg", "https://store.steampowered.com/app/427520/"),
                        ("Mutlu", "Hollow Knight", "Kendi yolunuzu çizin.", "https://cdn.akamai.steamstatic.com/steam/apps/367520/header.jpg", "https://store.steampowered.com/app/367520/"),
                        ("Mutlu", "Celeste", "Madeline'ın hayatta kalmasına yardım edin.", "https://cdn.akamai.steamstatic.com/steam/apps/504230/header.jpg", "https://store.steampowered.com/app/504230/"),
                        ("Gergin", "S.T.A.L.K.E.R. 2", "Dışlanma Bölgesi.", "https://cdn.akamai.steamstatic.com/steam/apps/1643320/header.jpg", "https://store.steampowered.com/app/1643320/"),
                        ("Mutlu", "Truck Simulator 2", "Avrupa'yı baştan başa dolaşın.", "https://cdn.akamai.steamstatic.com/steam/apps/227300/header.jpg", "https://store.steampowered.com/app/227300/"),
                        ("Enerjik", "DOOM Eternal", "Cehennemi yerle bir edin.", "https://cdn.akamai.steamstatic.com/steam/apps/782330/header.jpg", "https://store.steampowered.com/app/782330/"),
                        ("Gergin", "Resident Evil 4", "Hayatta kalmak sadece başlangıç.", "https://cdn.akamai.steamstatic.com/steam/apps/1196590/header.jpg", "https://store.steampowered.com/app/1196590/"),
                        ("Gergin", "Outer Wilds", "Güneş sistemini keşfedin.", "https://cdn.akamai.steamstatic.com/steam/apps/753640/header.jpg", "https://store.steampowered.com/app/753640/")
                    });

                    // --- FİLMLER (50+ Netflix Movies) ---
                    AddBatch(con, sql, "FİLMLER", new[] {
                        // Heyecanlı (Excited)
                        ("Heyecanlı", "Extraction 2", "Tyler Rake tehlikeli bir hapishaneden aile kurtarır", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81098494"),
                        ("Heyecanlı", "Red Notice", "FBI ajanı ve hırsızlar dünya çapında kovalamaca", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81161626"),
                        ("Heyecanlı", "The Gray Man", "CIA ajanı karanlık sırları açığa çıkarır", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81132437"),
                        ("Heyecanlı", "Army of the Dead", "Las Vegas'ta zombilere karşı soygun", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81046394"),
                        ("Heyecanlı", "6 Underground", "Milyarder yönetiminde gizli savaşçı ekibi", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81001887"),
                        ("Heyecanlı", "Bullet Train", "Hızlı trende ölümcül görev", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81277951"),
                        ("Heyecanlı", "Heart of Stone", "Süper ajan dünyayı korumaya çalışır", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81226647"),
                        ("Heyecanlı", "The Adam Project", "Zaman yolculuğu macerası", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81309354"),
                        ("Heyecanlı", "Tyler Rake", "Kiralık asker çocuk kurtarma operasyonu", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/80230399"),
                        ("Heyecanlı", "The Old Guard", "Ölümsüz savaşçıların aksiyonu", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81038963"),
                        ("Heyecanlı", "Spiderhead", "Deneysel ilaçlarla kontrol edilen mahkumlar", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/80232926"),
                        ("Heyecanlı", "The Mother", "Eski suikastçi kızını korur", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81161626"),
                        
                        // Mutlu / Keyifli (Happy)
                        ("Mutlu", "Glass Onion", "Gizemli ada cinayeti", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81458416"),
                        ("Mutlu", "Murder Mystery", "Tatilde cinayet gizemi", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80242619"),
                        ("Mutlu", "Murder Mystery 2", "Paris'te yeni gizem", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81161626"),
                        ("Mutlu", "The Kissing Booth", "Gençlik romantik komedisi", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81026700"),
                        ("Mutlu", "Always Be My Maybe", "Eski aşıklar yeniden buluşur", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80202874"),
                        ("Mutlu", "To All the Boys I've Loved Before", "Gizli aşk mektupları ifşa olur", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80203147"),
                        ("Mutlu", "The Half of It", "Modern Cyrano hikayesi", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81026734"),
                        ("Mutlu", "Eurovision Song Contest", "Müzik yarışması komedisi", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80244088"),
                        ("Mutlu", "Yes Day", "Ailece evet günü macerası", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81026759"),
                        ("Mutlu", "The Lovebirds", "Çifte cinayet gizemi", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81026729"),
                        ("Mutlu", "Happiness for Beginners", "Kendini keşfetme yolculuğu", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81312891"),
                        ("Mutlu", "Love at First Sight", "Havaalanında başlayan aşk", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81460627"),
                        
                        // Gergin / Stresli (Tense/Stressed)
                        ("Stresli", "Leave the World Behind", "Küresel felaket gerilimi", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/81314956"),
                        ("Stresli", "Don't Look Up", "Dünyayı yok edecek kuyruklu yıldız", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/81252357"),
                        ("Stresli", "The Platform", "Dikey hapishanede hayatta kalma", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/81128579"),
                        ("Stresli", "Bird Box", "Gözlerini açarsan ölürsün", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80196789"),
                        ("Stresli", "Fractured", "Kayıp aile gizemli hastane", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/81020662"),
                        ("Stresli", "His House", "Hayaletli evde mülteciler", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/81078819"),
                        ("Stresli", "The Guilty", "Telefonda kurtarma operasyonu", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/81425814"),
                        ("Stresli", "Earthquake Bird", "Tokyo'da gizemli cinayet", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80211495"),
                        ("Stresli", "The Stranger", "Bir yabancı sırları ifşa eder", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/81001209"),
                        ("Stresli", "Calibre", "İskoçya'da av kazası", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80994052"),
                        
                        // Üzgün (Sad)
                        ("Üzgün", "Marriage Story", "Boşanma draması", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/80223779"),
                        ("Üzgün", "Pieces of a Woman", "Trajik kayıp sonrası", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/81074137"),
                        ("Üzgün", "The Power of the Dog", "Karanlık Western draması", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/81127997"),
                        ("Üzgün", "All Quiet on the Western Front", "Savaşın dehşeti", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/81260280"),
                        ("Üzgün", "The Two Popes", "İki Papa arasında diyalog", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/80174451"),
                        ("Üzgün", "Roma", "1970'ler Meksika'sında aile", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/80240715"),
                        ("Üzgün", "Blonde", "Marilyn Monroe'nun trajik hayatı", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/80991789"),
                        
                        // Yorgun (Tired)
                        ("Yorgun", "The Midnight Sky", "Sessiz post-kıyamet hikayesi", @"Database\Posters\netflix_tired_poster_1767339023887.png", "https://www.netflix.com/title/80244645"),
                        ("Yorgun", "Okja", "Kız ve süper domuzu", @"Database\Posters\netflix_tired_poster_1767339023887.png", "https://www.netflix.com/title/80091936"),
                        ("Yorgun", "I'm Thinking of Ending Things", "Sürrealist yolculuk", @"Database\Posters\netflix_tired_poster_1767339023887.png", "https://www.netflix.com/title/80211559"),
                        ("Yorgun", "The Dig", "Sakin arkeoloji keşfi", @"Database\Posters\netflix_tired_poster_1767339023887.png", "https://www.netflix.com/title/81167887"),
                        
                        // Öfkeli (Angry)
                        ("Öfkeli", "Luther: The Fallen Sun", "Dedektif intikam peşinde", @"Database\Posters\netflix_angry_poster_1767339043816.png", "https://www.netflix.com/title/81161626"),
                        ("Öfkeli", "The Harder They Fall", "Intikam Western", @"Database\Posters\netflix_angry_poster_1767339043816.png", "https://www.netflix.com/title/81041966"),
                        ("Öfkeli", "Rebel Ridge", "Yozlaşmış kasabada adalet", @"Database\Posters\netflix_angry_poster_1767339043816.png", "https://www.netflix.com/title/81098494"),
                        ("Öfkeli", "White Noise", "Toksik olay sonrası kaos", @"Database\Posters\netflix_angry_poster_1767339043816.png", "https://www.netflix.com/title/80985648"),
                        
                        // Yalnız (Lonely)
                        ("Yalnız", "Bruised", "Yalnız dövüşçü geri dönüş", @"Database\Posters\netflix_lonely_poster_1767339061024.png", "https://www.netflix.com/title/81043755"),
                        ("Yalnız", "Lost Girls", "Kayıp kızlar gizemi", @"Database\Posters\netflix_lonely_poster_1767339061024.png", "https://www.netflix.com/title/80219558"),
                        
                        // Sıkılmış (Bored)
                        ("Sıkılmış", "Enola Holmes", "Sherlock'un kız kardeşinin macerası", @"Database\Posters\netflix_bored_poster_1767339076586.png", "https://www.netflix.com/title/81277950"),
                        ("Sıkılmış", "Enola Holmes 2", "Yeni gizem macerası", @"Database\Posters\netflix_bored_poster_1767339076586.png", "https://www.netflix.com/title/81260280"),
                        ("Sıkılmış", "The Mitchells vs. The Machines", "Aile robot isyanına karşı", @"Database\Posters\netflix_bored_poster_1767339076586.png", "https://www.netflix.com/title/81399614"),
                        ("Sıkılmış", "Klaus", "Noel köyünde sihir", @"Database\Posters\netflix_bored_poster_1767339076586.png", "https://www.netflix.com/title/80183187")
                    });

                    // --- DİZİLER (50+ Netflix Series) ---
                    AddBatch(con, sql, "DİZİLER", new[] {
                        // Heyecanlı (Excited)
                        ("Heyecanlı", "Squid Game", "Ölümcül oyunlar turnuvası", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81040344"),
                        ("Heyecanlı", "Money Heist", "Büyük banka soygunu", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/80192098"),
                        ("Heyecanlı", "The Witcher", "Canavar avcısı Geralt", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/80189685"),
                        ("Heyecanlı", "Stranger Things", "80'lerde doğaüstü gizem", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/80057281"),
                        ("Heyecanlı", "The Umbrella Academy", "Süper güçlü kardeşler", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/80186863"),
                        ("Heyecanlı", "Cobra Kai", "Karate Kid yıllar sonra", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81002370"),
                        ("Heyecanlı", "Sweet Tooth", "Melez çocuk post-kıyamet", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81237854"),
                        ("Heyecanlı", "Shadow and Bone", "Karanlık güçler fantezisi", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/80236319"),
                        ("Heyecanlı", "Wednesday", "Addams ailesi kızı okula", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81231974"),
                        ("Heyecanlı", "The Last Kingdom", "Vikingler çağı savaşları", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/80074249"),
                        ("Heyecanlı", "Arcane", "League of Legends evreni", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/81435684"),
                        ("Heyecanlı", "Alice in Borderland", "Tokyo'da ölümcül oyunlar", @"Database\Posters\netflix_excited_poster_1767338957326.png", "https://www.netflix.com/title/80200807"),
                        
                        // Mutlu / Keyifli (Happy)
                        ("Mutlu", "Emily in Paris", "Paris'te genç pazarlamacı", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81037371"),
                        ("Mutlu", "The Good Place", "Ölümden sonra komedi", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80113701"),
                        ("Mutlu", "Never Have I Ever", "Hintli-Amerikan gencin hayatı", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80179190"),
                        ("Mutlu", "Heartstopper", "LGBTQ+ gençlik romanı", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81059939"),
                        ("Mutlu", "Schitt's Creek", "Zengin aile kasabada", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80036165"),
                        ("Mutlu", "Grace and Frankie", "Yaşlı kadınlar yeni hayat", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80017537"),
                        ("Mutlu", "The Chef Show", "Şefler yemek yapar", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81026108"),
                        ("Mutlu", "Queer Eye", "Fab 5 hayat değiştiriyor", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80160037"),
                        ("Mutlu", "The Crown", "Kraliyet ailesi draması", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80025678"),
                        ("Mutlu", "Virgin River", "Küçük kasaba romantizmi", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80198655"),
                        ("Mutlu", "Love is Blind", "Gözler görmeden aşk", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/80996601"),
                        ("Mutlu", "The Circle", "Sosyal medya yarışması", @"Database\Posters\netflix_happy_poster_1767338974251.png", "https://www.netflix.com/title/81044551"),
                        
                        // Stresli (Stressed)
                        ("Stresli", "Ozark", "Para aklama gerilimi", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80117552"),
                        ("Stresli", "Breaking Bad", "Kimya öğretmeni uyuşturucu", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/70143836"),
                        ("Stresli", "Narcos", "Pablo Escobar hikayesi", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80025172"),
                        ("Stresli", "Peaky Blinders", "Birmingham çetesi 1900'ler", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80002479"),
                        ("Stresli", "You", "Takıntılı katil aşık", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80211991"),
                        ("Stresli", "The Sinner", "Psikolojik suç draması", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80175802"),
                        ("Stresli", "Mindhunter", "Seri katil profilleri", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80114855"),
                        ("Stresli", "Dark", "Zaman yolculuğu gizemi", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80100172"),
                        ("Stresli", "1899", "Gizemli gemi yolculuğu", @"Database\Posters\netflix_stressed_poster_1767338990086.png", "https://www.netflix.com/title/80154558"),
                        
                        // Üzgün (Sad)
                        ("Üzgün", "13 Reasons Why", "İntihar sebepleri", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/80117470"),
                        ("Üzgün", "After Life", "Eşini kaybetmiş adam", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/80998491"),
                        ("Üzgün", "The Queen's Gambit", "Yetim satranç dahisi", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/80234304"),
                        ("Üzgün", "Maid", "Yoksulluktan kurtuluş mücadelesi", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/81166770"),
                        ("Üzgün", "When They See Us", "Yanlış hüküm trajedisi", @"Database\Posters\netflix_sad_poster_1767339007629.png", "https://www.netflix.com/title/80200549"),
                        
                        // Yorgun (Tired)
                        ("Yorgun", "The Midnight Gospel", "Felsefik animasyon", @"Database\Posters\netflix_tired_poster_1767339023887.png", "https://www.netflix.com/title/80987903"),
                        ("Yorgun", "Midnight Mass", "Adada gizemli olaylar", @"Database\Posters\netflix_tired_poster_1767339023887.png", "https://www.netflix.com/title/81083626"),
                        ("Yorgun", "Maniac", "Psikolojik deney", @"Database\Posters\netflix_tired_poster_1767339023887.png", "https://www.netflix.com/title/80124522"),
                        
                        // Öfkeli (Angry)
                        ("Öfkeli", "The Punisher", "İntikamcı anti-kahraman", @"Database\Posters\netflix_angry_poster_1767339043816.png", "https://www.netflix.com/title/80117498"),
                        ("Öfkeli", "Bodyguard", "Koruma tehdit altında", @"Database\Posters\netflix_angry_poster_1767339043816.png", "https://www.netflix.com/title/80235864"),
                        ("Öfkeli", "Clickbait", "Viral video gizemi", @"Database\Posters\netflix_angry_poster_1767339043816.png", "https://www.netflix.com/title/81131630"),
                        
                        // Yalnız (Lonely)
                        ("Yalnız", "Russian Doll", "Zaman döngüsünde yalnız", @"Database\Posters\netflix_lonely_poster_1767339061024.png", "https://www.netflix.com/title/80211627"),
                        ("Yalnız", "The OA", "Kayıp kadın geri döner", @"Database\Posters\netflix_lonely_poster_1767339061024.png", "https://www.netflix.com/title/80044950"),
                        ("Yalnız", "Living with Yourself", "Klonlanmış adam", @"Database\Posters\netflix_lonely_poster_1767339061024.png", "https://www.netflix.com/title/80178724"),
                        
                        // Sıkılmış (Bored)
                        ("Sıkılmış", "Black Mirror", "Teknoloji distopyası", @"Database\Posters\netflix_bored_poster_1767339076586.png", "https://www.netflix.com/title/70264888"),
                        ("Sıkılmış", "Love, Death & Robots", "Animasyon antolojisi", @"Database\Posters\netflix_bored_poster_1767339076586.png", "https://www.netflix.com/title/80174608"),
                        ("Sıkılmış", "The Sandman", "Rüya lordu macerası", @"Database\Posters\netflix_bored_poster_1767339076586.png", "https://www.netflix.com/title/81150303"),
                        ("Sıkılmış", "Locke & Key", "Sihirli anahtarlar evi", @"Database\Posters\netflix_bored_poster_1767339076586.png", "https://www.netflix.com/title/80241239"),
                        ("Sıkılmış", "The Haunting of Hill House", "Hayaletli ev ailesi", @"Database\Posters\netflix_bored_poster_1767339076586.png", "https://www.netflix.com/title/80189221")
                    });

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        private static void AddBatch(SQLiteConnection con, string sql, string category, (string mood, string title, string desc, string img, string link)[] items)
        {
            foreach (var item in items)
            {
                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@c", category);
                    cmd.Parameters.AddWithValue("@m", item.mood);
                    cmd.Parameters.AddWithValue("@t", item.title);
                    cmd.Parameters.AddWithValue("@d", item.desc);
                    cmd.Parameters.AddWithValue("@i", item.img);
                    cmd.Parameters.AddWithValue("@a", item.link);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void AddSingle(SQLiteConnection con, string sql, string cat, string mood, string title, string desc, string img, string link)
        {
            using (var cmd = new SQLiteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@c", cat);
                cmd.Parameters.AddWithValue("@m", mood);
                cmd.Parameters.AddWithValue("@t", title);
                cmd.Parameters.AddWithValue("@d", desc);
                cmd.Parameters.AddWithValue("@i", img);
                cmd.Parameters.AddWithValue("@a", link);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
