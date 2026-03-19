CREATE DATABASE AyudaExamen;

USE AyudaExamen;

CREATE TABLE usuarios (
    id INT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    correo VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    salt VARCHAR(255) NOT NULL
);

CREATE TABLE comics (
    id INT PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    autor VARCHAR(100) NOT NULL,
    anio INT, 
    descripcion TEXT
);

CREATE TABLE imagenes (
    comic_id INT REFERENCES comics(id) ON DELETE CASCADE,
    imagen_url VARCHAR(255) NOT NULL
);

INSERT INTO comics (id, nombre, autor, anio, descripcion) VALUES 
(1, 'Batman: The Dark Knight Returns', 'Frank Miller', 1986, 'Un Bruce Wayne de 55 años vuelve de su retiro para salvar a una Gotham sumida en el caos.'),
(2, 'Watchmen', 'Alan Moore', 1986, 'El asesinato de un antiguo compañero obliga a un grupo de vigilantes retirados a salir de las sombras.'),
(3, 'Spider-Man: La última cacería de Kraven', 'J.M. DeMatteis', 1987, 'Kraven el Cazador busca su victoria definitiva sobre Spider-Man.');

INSERT INTO imagenes (comic_id, imagen_url) VALUES 
(1, 'https://covers.openlibrary.org/b/isbn/9781563893421-L.jpg'),
(1, 'https://covers.openlibrary.org/b/isbn/9781401263119-L.jpg'),
(1, 'https://covers.openlibrary.org/b/isbn/9781852867980-L.jpg'),
(2, 'https://covers.openlibrary.org/b/isbn/9781401248192-L.jpg'),
(2, 'https://covers.openlibrary.org/b/isbn/9788418225710-L.jpg'),
(2, 'https://covers.openlibrary.org/b/isbn/9788416518968-L.jpg'),
(3, 'https://covers.openlibrary.org/b/isbn/9780785134503-L.jpg'),
(3, 'https://covers.openlibrary.org/b/isbn/9781302923747-L.jpg'),
(3, 'https://covers.openlibrary.org/b/isbn/9781302911843-L.jpg');