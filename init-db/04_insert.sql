INSERT INTO "desafio-backend".motorcycles (manufacturing_year,model,brand,plate,created_at,updated_at) VALUES
	 (2024,'teste','teste','GHR-5j87','2024-09-13 03:04:12.7279','2024-09-13 03:04:12.7279'),
	 (2020,'testeqqq','brand','GTY-1212','2024-09-13 06:18:31.621','2024-09-13 06:18:31.621'),
	 (2012,'yamaha','cg400','KAH-YDE4',NULL,NULL),
	 (2024,'honda','kie','AAA-AAAE',NULL,NULL),
	 (2020,'honda','rer','AAA-ERED',NULL,NULL),
	 (2024,'honda','rer','AAA-ERRE',NULL,NULL),
	 (2024,'honda','rer','AAA-FGGH',NULL,NULL),
	 (2024,'honda','rer','AAA-KKKK',NULL,NULL),
	 (2024,'honda','rer','AAA-WERA',NULL,NULL);

INSERT INTO "desafio-backend".price_range (max_days,price_per_day,penalty_rate) VALUES
	 (30,22.00,NULL),
	 (45,20.00,NULL),
	 (50,18.00,NULL),
	 (7,30.00,20),
	 (15,28.00,40);

INSERT INTO "desafio-backend".drivers ("name",cnpj,birth_date,driver_license_number,driver_license_type,driver_license_image_url,created_at,updated_at) VALUES
	 ('José Silveira','27745648000143','2024-09-14','321321321','A',NULL,'2024-09-14 06:18:44.248707','2024-09-14 06:18:44.248708');

