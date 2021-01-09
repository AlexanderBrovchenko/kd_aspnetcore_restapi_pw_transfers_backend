-- --------------------------------------------------------
-- Ip server:                         127.0.0.1
-- Created in application:               10.1.5-MariaDB - mariadb.org binary distribution
-- Operating system:                   Win64
-- HeidiSQL version:              9.1.0.4867
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Database to be created kd_pw_transfers
CREATE DATABASE IF NOT EXISTS `kd_pw_transfers` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_general_ci */;
USE `kd_pw_transfers`;

-- Table to be added: kd_pw_transfers.users
CREATE TABLE IF NOT EXISTS `users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `Email` varchar(50) COLLATE latin1_general_ci NOT NULL,
  `PasswordHash` varchar(255) COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name` (`Name`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;


-- Table to be added: kd_pw_transfers.transfers
CREATE TABLE IF NOT EXISTS `transfers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PayerId` int(11) DEFAULT NULL,
  `PayeeId` int(11) NOT NULL,
  `Amount` int(11) NOT NULL,
  `OperatedAt` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_transfers_users` (`PayerId`),
  KEY `FK_transfers_users_2` (`PayeeId`),
  CONSTRAINT `FK_transfers_users` FOREIGN KEY (`PayerId`) REFERENCES `users` (`Id`),
  CONSTRAINT `FK_transfers_users_2` FOREIGN KEY (`PayeeId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

