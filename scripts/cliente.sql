CREATE TABLE `enderecos` (
  `id` INT NOT NULL,
  `cidade_id` INT NOT NULL,
  `cep` VARCHAR(9) NOT NULL,
  `bairro` VARCHAR(100) NOT NULL,
  `rua` VARCHAR(100) NOT NULL,
  `numero` INT NULL,
  `complemento` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_endereco_cidade` FOREIGN KEY (`cidade_id`) REFERENCES `cidades` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE `sexo` (
  `id` INT NOT NULL,
  `genero` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


CREATE TABLE `clientes` (
  `id` INT NOT NULL,
  `sexo_id` INT NOT NULL,
  `nome` VARCHAR(100) NOT NULL,
  `data_nascimento` DATE NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_cliente_sexo` FOREIGN KEY (`sexo_id`) REFERENCES `sexo` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE `endereco_cliente` (
  `endereco_id` INT NOT NULL,
  `cliente_id` INT NOT NULL,
  PRIMARY KEY (`endereco_id`, `cliente_id`),
  CONSTRAINT `fk_endereco_cliente_endereco` FOREIGN KEY (`endereco_id`) REFERENCES `enderecos` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_endereco_cliente_cliente` FOREIGN KEY (`cliente_id`)  REFERENCES `clientes` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

INSERT INTO `sexo` (`id`, `genero`) VALUES
(1, 'Homem'),
(2, 'Mulher'),
(3, 'Outro');