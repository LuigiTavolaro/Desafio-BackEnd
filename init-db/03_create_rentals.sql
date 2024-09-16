CREATE TABLE IF NOT EXISTS "desafio-backend".rentals (
    id SERIAL PRIMARY KEY,
    motorcycle_id INT NOT NULL,
    driver_id INT NOT NULL,
    price_range_id INT NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    expected_end_date DATE NOT NULL,
    created_at TIMESTAMP DEFAULT NOW(),
    updated_at TIMESTAMP DEFAULT NOW(),
    CONSTRAINT fk_motorcycle FOREIGN KEY (motorcycle_id) REFERENCES "desafio-backend".motorcycles(id),
    CONSTRAINT fk_driver FOREIGN KEY (driver_id) REFERENCES "desafio-backend".drivers(id),
    CONSTRAINT fk_price_range FOREIGN KEY (price_range_id) REFERENCES "desafio-backend".price_range(id)
);