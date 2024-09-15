CREATE TABLE motorcycles (
    id SERIAL PRIMARY KEY,              -- Identificador único da moto (chave primária)
    manufacturing_year INT NOT NULL,    -- Ano de fabricação da moto (obrigatório)
    model VARCHAR(100) NOT NULL,        -- Modelo da moto (obrigatório)
    brand VARCHAR(100),                 -- Marca da moto (opcional)
    plate VARCHAR(20) NOT NULL,         -- Placa da moto (obrigatório)
    created_at TIMESTAMP DEFAULT NOW(), -- Data de criação (opcional, com valor padrão de data atual)
    updated_at TIMESTAMP DEFAULT NOW()  -- Data de atualização (opcional, com valor padrão de data atual)
);
CREATE TABLE rentals (
    id SERIAL PRIMARY KEY,              -- Identificador único da locação (chave primária)
    motorcycle_id INT NOT NULL,         -- Identificador da moto (chave estrangeira)
    driver_id INT NOT NULL,             -- Identificador do entregador (chave estrangeira)
    start_date DATE NOT NULL,           -- Data de início da locação (obrigatório)
    end_date DATE NOT NULL,             -- Data de término da locação (obrigatório)
    expected_end_date DATE NOT NULL,    -- Data de previsão de término da locação (obrigatório)
    created_at TIMESTAMP DEFAULT NOW(), -- Data de criação do registro (valor padrão: agora)
    updated_at TIMESTAMP DEFAULT NOW(), -- Data de atualização do registro (valor padrão: agora)
    
    CONSTRAINT fk_motorcycle
        FOREIGN KEY (motorcycle_id)
        REFERENCES motorcycles(id)      -- Referência à tabela de motos
        
    CONSTRAINT fk_driver
        FOREIGN KEY (driver_id)
        REFERENCES drivers(id)      -- Referência à tabela de motos
);


CREATE TABLE drivers (
    id SERIAL PRIMARY KEY,                        -- Identificador único do entregador
    name VARCHAR(150) NOT NULL,                   -- Nome do entregador
    cnpj VARCHAR(14) NOT NULL UNIQUE,             -- CNPJ único do entregador (14 caracteres)
    birth_date DATE NOT NULL,                     -- Data de nascimento
    driver_license_number VARCHAR(20) NOT NULL UNIQUE, -- Número da CNH, único
    driver_license_type VARCHAR(3) NOT NULL , -- Tipo da CNH, válido apenas 'A', 'B' ou 'A+B'
    driver_license_image_url VARCHAR(255),        -- URL da imagem da CNH armazenada externamente (formato .png ou .bmp)
    created_at TIMESTAMP DEFAULT NOW(),           -- Data de criação do cadastro
    updated_at TIMESTAMP DEFAULT NOW()            -- Data de atualização do cadastro
);

CREATE TABLE price_range (
    id SERIAL PRIMARY KEY,
    max_days INT NOT NULL,
    penalty_rate int null, 
    price_per_day DECIMAL(10, 2) NOT NULL
);

INSERT INTO price_range (max_days, price_per_day, penalty_rate) VALUES
(7, 30.00. 20),
(15, 28.00. 40),
(30, 22.00, null),
(45, 20.00, null),
(50, 18.00, null);
