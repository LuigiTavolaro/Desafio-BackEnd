DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_tables WHERE schemaname = 'desafio-backend' AND tablename = 'motorcycles') THEN
        CREATE TABLE "desafio-backend".motorcycles (
            id SERIAL PRIMARY KEY,
            manufacturing_year INT NOT NULL,
            model VARCHAR(100) NOT NULL,
            brand VARCHAR(100),
            plate VARCHAR(20) NOT NULL,
            created_at TIMESTAMP DEFAULT NOW(),
            updated_at TIMESTAMP DEFAULT NOW()
        );
    END IF;
END $$;


DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_tables WHERE schemaname = 'desafio-backend' AND tablename = 'drivers') THEN
        CREATE TABLE "desafio-backend".drivers (
            id SERIAL PRIMARY KEY,
            name VARCHAR(150) NOT NULL,
            cnpj VARCHAR(14) NOT NULL UNIQUE,
            birth_date DATE NOT NULL,
            driver_license_number VARCHAR(20) NOT NULL UNIQUE,
            driver_license_type VARCHAR(3) NOT NULL,
            driver_license_image_url VARCHAR(255),
            created_at TIMESTAMP DEFAULT NOW(),
            updated_at TIMESTAMP DEFAULT NOW()
        );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_tables WHERE schemaname = 'desafio-backend' AND tablename = 'price_range') THEN
        CREATE TABLE "desafio-backend".price_range (
            id SERIAL PRIMARY KEY,
            max_days INT NOT NULL,
            penalty_rate INT NULL,
            price_per_day DECIMAL(10, 2) NOT NULL
        );
    END IF;
END $$;