CREATE TABLE "roles" (
     "id" UUID PRIMARY KEY NOT NULL,
     "name" VARCHAR(255) UNIQUE NOT NULL,
     "description" VARCHAR(255) DEFAULT 'Đang cập nhật',
     "disable" BOOL NOT NULL DEFAULT FALSE,
     "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
     "modified_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL
);

CREATE TABLE "administrators" (
                                  "id" UUID PRIMARY KEY NOT NULL,
                                  "user_name" VARCHAR(255) NOT NULL,
                                  "email" VARCHAR(255) UNIQUE NOT NULL,
                                  "phone_number" VARCHAR(11) UNIQUE NOT NULL,
                                  "dob" date NOT NULL,
                                  "gender" BOOL NOT NULL,
                                  "avatar_url" TEXT DEFAULT NULL,
                                  "password" TEXT NOT NULL,
                                  "permission" jsonb NOT NULL,
                                  "lockout_end" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL,
                                  "access_failed_count" INT DEFAULT 0,
                                  "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                                  "modified_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL,
                                  "role_id" UUID NOT NULL,
                                  "lock_acc" BOOL NOT NULL DEFAULT FALSE,
                                  CONSTRAINT "fk_administrators_role_id" FOREIGN key ("role_id") REFERENCES "roles" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "users" (
                         "id" UUID PRIMARY KEY NOT NULL,
                         "user_name" VARCHAR(255) NOT NULL,
                         "email" VARCHAR(255) UNIQUE NOT NULL,
                         "phone_number" VARCHAR(11) UNIQUE NOT NULL,
                         "dob" date NOT NULL,
                         "gender" BOOL NOT NULL,
                         "avatar_url" TEXT DEFAULT NULL,
                         "password" TEXT NOT NULL,
                         "lockout_end" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL,
                         "access_failed_count" INT DEFAULT 0,
                         "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                         "modified_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL,
                         "role_id" UUID NOT NULL,
                         "lock_acc" BOOL NOT NULL DEFAULT FALSE,
                         CONSTRAINT "fk_users_role_id" FOREIGN key ("role_id") REFERENCES "roles" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "admins_refresh_token"(
                                       "id" BIGSERIAL PRIMARY KEY NOT NULL,
                                       "refresh_token" TEXT NOT NULL,
                                       "expires_at" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
                                       "admin_id" UUID UNIQUE NOT NULL,
                                       CONSTRAINT "fk_administrators_id" FOREIGN KEY (admin_id) REFERENCES administrators (id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE "users_refresh_token"(
                                      "id" BIGSERIAL PRIMARY KEY NOT NULL,
                                      "refresh_token" TEXT NOT NULL,
                                      "expires_at" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
                                      "user_id" UUID UNIQUE NOT NULL,
                                      CONSTRAINT "fk_users_id" FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE "artists" (
                           "id" BIGSERIAL PRIMARY KEY NOT NULL,
                           "name" VARCHAR(255) NOT NULL,
                           "alias" VARCHAR(255) UNIQUE NOT NULL,
                           "avatar_url" TEXT DEFAULT NULL,
                           "thumbnail_url" TEXT DEFAULT NULL,
                           "description" TEXT DEFAULT 'Đang cập nhật',
                           "reward" INT DEFAULT 0,
                           "dob" date DEFAULT CURRENT_DATE,
                           "country" VARCHAR(255) NOT NULL,
                           "disable" BOOL NOT NULL DEFAULT FALSE,
                           "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                           "modified_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL
);

CREATE TABLE "users_follow_artists" (
                                        "user_id" UUID NOT NULL,
                                        "artist_id" bigint NOT NULL,
                                        PRIMARY KEY ("user_id", "artist_id"),
                                        CONSTRAINT "fk_users_follow_artists_user_id" FOREIGN key ("user_id") REFERENCES "users" ("id") ON DELETE cascade ON UPDATE cascade,
                                        CONSTRAINT "fk_users_follow_artists_artist_id" FOREIGN key ("artist_id") REFERENCES "artists" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "playlists" (
                             "id" BIGSERIAL PRIMARY KEY NOT NULL,
                             "name" VARCHAR(255) NOT NULL,
                             "alias" VARCHAR(255) UNIQUE NOT NULL,
                             "avatar_url" TEXT NOT NULL,
                             "release_date" date NOT NULL DEFAULT CURRENT_DATE,
                             "description" TEXT DEFAULT 'Đang cập nhật',
                             "disable" BOOL NOT NULL DEFAULT FALSE,
                             "tag" VARCHAR(255) DEFAULT NULL,
                             "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                             "modified_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT  NULL
);

CREATE TABLE "playlists_artists" (
                                     "artist_id" bigint NOT NULL,
                                     "playlist_id" bigint NOT NULL,
                                     PRIMARY KEY ("artist_id", "playlist_id"),
                                     CONSTRAINT "fk_playlists_artists_artist_id" FOREIGN key ("artist_id") REFERENCES "artists" ("id") ON DELETE cascade ON UPDATE cascade,
                                     CONSTRAINT "fk_playlists_artists_playlist_id" FOREIGN key ("playlist_id") REFERENCES "playlists" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "users_follow_playlists" (
                                          "user_id" UUID NOT NULL,
                                          "playlist_id" bigint NOT NULL,
                                          PRIMARY KEY ("user_id", "playlist_id"),
                                          CONSTRAINT "fk_users_follow_playlists_user_id" FOREIGN key ("user_id") REFERENCES "users" ("id") ON DELETE cascade ON UPDATE cascade,
                                          CONSTRAINT "fk_users_follow_playlists_playlist_id" FOREIGN key ("playlist_id") REFERENCES "playlists" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "albums" (
                          "id" BIGSERIAL PRIMARY KEY NOT NULL,
                          "name" VARCHAR(255) NOT NULL,
                          "alias" VARCHAR(255) UNIQUE NOT NULL,
                          "avatar_url" TEXT DEFAULT NULL,
                          "release_date" date NOT NULL DEFAULT CURRENT_DATE,
                          "description" TEXT DEFAULT 'Đang cập nhật',
                          "tag" VARCHAR(255) DEFAULT NULL,
                          "producer" VARCHAR(255) DEFAULT NULL,
                          "disable" BOOL NOT NULL DEFAULT FALSE,
                          "duration" DECIMAL NOT NULL DEFAULT 0,
                          "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                          "modified_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL
);

CREATE TABLE "users_follow_albums" (
                                       "user_id" UUID NOT NULL,
                                       "album_id" bigint NOT NULL,
                                       PRIMARY KEY ("user_id", "album_id"),
                                       CONSTRAINT "fk_users_follow_albums_user_id" FOREIGN key ("user_id") REFERENCES "users" ("id") ON DELETE cascade ON UPDATE cascade,
                                       CONSTRAINT "fk_users_follow_albums_album_id" FOREIGN key ("album_id") REFERENCES "albums" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "albums_artists" (
                                  "album_id" bigint NOT NULL,
                                  "artist_id" bigint NOT NULL,
                                  PRIMARY KEY ("album_id", "artist_id"),
                                  CONSTRAINT "fk_albums_artists_album_id" FOREIGN key ("album_id") REFERENCES "albums" ("id") ON DELETE cascade ON UPDATE cascade,
                                  CONSTRAINT "fk_albums_artists_artist_id" FOREIGN key ("artist_id") REFERENCES "artists" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "themes" (
                          "id" BIGSERIAL PRIMARY KEY NOT NULL,
                          "name" VARCHAR(255) NOT NULL,
                          "alias" VARCHAR(255) UNIQUE NOT NULL,
                          "disable" BOOLEAN NOT NULL DEFAULT FALSE,
                          "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                          "modified_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL
);

CREATE TABLE "playlists_themes" (
                                    "playlist_id" bigint NOT NULL,
                                    "theme_id" bigint NOT NULL,
                                    PRIMARY KEY ("playlist_id", "theme_id"),
                                    CONSTRAINT "fk_playlists_themes_playlist_id" FOREIGN key ("playlist_id") REFERENCES "playlists" ("id") ON DELETE cascade ON UPDATE cascade,
                                    CONSTRAINT "fk_playlists_themes_theme_id" FOREIGN key ("theme_id") REFERENCES "themes" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "songs" (
                         "id" BIGSERIAL PRIMARY KEY NOT NULL,
                         "name" VARCHAR(255) NOT NULL,
                         "alias" VARCHAR(255) UNIQUE NOT NULL,
                         "avatar_url" TEXT NOT NULL,
                         "release_date" date NOT NULL,
                         "view" BIGINT NOT NULL DEFAULT 0,
                         "description" TEXT DEFAULT 'Đang cập nhật',
                         "duration" INT NOT NULL DEFAULT 0,
                         "lyric" TEXT DEFAULT 'Đang cập nhật.',
                         "album_id" bigint NOT NULL,
                         "language" VARCHAR(255) NOT NULL DEFAULT 'Đang cập nhật.',
                         "disable" BOOLEAN NOT NULL DEFAULT FALSE,
                         "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                         "modified_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL,
                         CONSTRAINT "fk_songs_album_id" FOREIGN key ("album_id") REFERENCES "albums" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "user_follow_songs" (
                                     "user_id" UUID NOT NULL,
                                     "song_id" bigint NOT NULL,
                                     PRIMARY KEY ("user_id", "song_id"),
                                     CONSTRAINT "fk_user_follow_songs_user_id" FOREIGN key ("user_id") REFERENCES "users" ("id") ON DELETE cascade ON UPDATE cascade,
                                     CONSTRAINT "fk_user_follow_songs_song_id" FOREIGN key ("song_id") REFERENCES "songs" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "artists_songs" (
                                 "artist_id" bigint NOT NULL,
                                 "song_id" bigint NOT NULL,
                                 PRIMARY KEY ("artist_id", "song_id"),
                                 CONSTRAINT "fk_artists_songs_artist_id" FOREIGN key ("artist_id") REFERENCES "artists" ("id") ON DELETE cascade ON UPDATE cascade,
                                 CONSTRAINT "fk_artists_songs_song_id" FOREIGN key ("song_id") REFERENCES "songs" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "genres" (
                          "id" BIGSERIAL PRIMARY KEY NOT NULL,
                          "name" VARCHAR(255) NOT NULL,
                          "alias" VARCHAR(255) UNIQUE NOT NULL,
                          "disable" BOOLEAN NOT NULL DEFAULT FALSE,
                          "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                          "modified_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT NULL
);

CREATE TABLE "songs_genres" (
                                "song_id" bigint NOT NULL,
                                "genres_id" bigint NOT NULL,
                                PRIMARY KEY ("song_id", "genres_id"),
                                CONSTRAINT "fk_songs_genres_song_id" FOREIGN key ("song_id") REFERENCES "songs" ("id") ON DELETE cascade ON UPDATE cascade,
                                CONSTRAINT "fk_songs_genres_genre_id" FOREIGN key ("genres_id") REFERENCES "genres" ("id") ON DELETE cascade ON UPDATE cascade
);

CREATE TABLE "playlists_songs" (
                                   "playlist_id" bigint NOT NULL,
                                   "song_id" bigint NOT NULL,
                                   PRIMARY KEY ("playlist_id", "song_id"),
                                   CONSTRAINT "fk_playlists_songs_playlist_id" FOREIGN key ("playlist_id") REFERENCES "playlists" ("id") ON DELETE cascade ON UPDATE cascade,
                                   CONSTRAINT "fk_playlists_songs_song_id" FOREIGN key ("song_id") REFERENCES "songs" ("id") ON DELETE cascade ON UPDATE cascade
);

-- Indexes
CREATE INDEX idx_users_email ON "users" (email);

CREATE INDEX idx_administrators_email ON "administrators" (email);

CREATE INDEX idx_songs_name ON "songs" (name);

CREATE INDEX idx_artists_name ON "artists" (name);

CREATE INDEX idx_playlists_name ON "playlists" (name);

CREATE INDEX idx_albums_name ON "albums" (name);

--Additional Indexes for Foreign Keys
CREATE INDEX idx_administrators_role_id ON administrators (role_id);

CREATE INDEX idx_users_role_id ON users (role_id);

CREATE INDEX idx_users_follow_artists_user_id ON users_follow_artists (user_id);

CREATE INDEX idx_users_follow_artists_artist_id ON users_follow_artists (artist_id);

CREATE INDEX idx_playlists_artists_artist_id ON playlists_artists (artist_id);

CREATE INDEX idx_playlists_artists_playlist_id ON playlists_artists (playlist_id);

CREATE INDEX idx_users_follow_playlists_user_id ON users_follow_playlists (user_id);

CREATE INDEX idx_users_follow_playlists_playlist_id ON users_follow_playlists (playlist_id);

CREATE INDEX idx_users_follow_albums_user_id ON users_follow_albums (user_id);

CREATE INDEX idx_users_follow_albums_album_id ON users_follow_albums (album_id);

CREATE INDEX idx_albums_artists_album_id ON albums_artists (album_id);

CREATE INDEX idx_albums_artists_artist_id ON albums_artists (artist_id);

CREATE INDEX idx_playlists_themes_playlist_id ON playlists_themes (playlist_id);

CREATE INDEX idx_playlists_themes_theme_id ON playlists_themes (theme_id);

CREATE INDEX idx_songs_album_id ON songs (album_id);

CREATE INDEX idx_user_follow_songs_user_id ON user_follow_songs (user_id);

CREATE INDEX idx_user_follow_songs_song_id ON user_follow_songs (song_id);

CREATE INDEX idx_artists_songs_artist_id ON artists_songs (artist_id);

CREATE INDEX idx_artists_songs_song_id ON artists_songs (song_id);

CREATE INDEX idx_songs_genres_song_id ON songs_genres (song_id);

CREATE INDEX idx_songs_genres_genre_id ON songs_genres (genres_id);

CREATE INDEX idx_playlists_songs_playlist_id ON playlists_songs (playlist_id);

CREATE INDEX idx_playlists_songs_song_id ON playlists_songs (song_id);