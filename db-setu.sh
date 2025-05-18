#!/bin/bash

DB_NAME="upeys"
DB_USER="postgres"
DB_PASS="111"
DB_HOST="localhost"
DB_PORT="5432"

echo "Veritabanı bağlantısı: $DB_USER@$DB_HOST:$DB_PORT/$DB_NAME"

# PostgreSQL kullanıcı ve veritabanı oluşturulması (eğer yoksa)
psql -U postgres -h "$DB_HOST" -tc "SELECT 1 FROM pg_roles WHERE rolname = '$DB_USER'" | grep -q 1 || \
    psql -U postgres -h "$DB_HOST" -c "CREATE USER $DB_USER WITH PASSWORD '$DB_PASS';"

psql -U postgres -h "$DB_HOST" -tc "SELECT 1 FROM pg_database WHERE datname = '$DB_NAME'" | grep -q 1 || \
    psql -U postgres -h "$DB_HOST" -c "CREATE DATABASE $DB_NAME OWNER $DB_USER;"

# Migration uygulama
export DefaultConnection="Host=$DB_HOST;Port=$DB_PORT;Database=$DB_NAME;Username=$DB_USER;Password=$DB_PASS;SearchPath=mes_db"
dotnet ef database update --project Infrastructure

