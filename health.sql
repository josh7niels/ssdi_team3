-- phpMyAdmin SQL Dump
-- version 4.2.11
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Oct 26, 2015 at 04:26 AM
-- Server version: 5.6.21
-- PHP Version: 5.6.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `health`
--

-- --------------------------------------------------------

--
-- Table structure for table `appointmentbookingdetail`
--

CREATE TABLE IF NOT EXISTS `appointmentbookingdetail` (
`appointment_id` int(11) NOT NULL,
  `physician_id` varchar(50) DEFAULT NULL,
  `patient_id` varchar(50) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `time` time DEFAULT NULL,
  `cause` varchar(200) DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `appointmentbookingdetail`
--

INSERT INTO `appointmentbookingdetail` (`appointment_id`, `physician_id`, `patient_id`, `date`, `time`, `cause`) VALUES
(1, 'mBilliska@uncc.edu', 'llopaudr@uncc.edu', '2015-10-27', '09:00:00', 'office visit'),
(2, 'mBilliska@uncc.edu', 'mnaga@uncc.edu', '2015-10-26', '11:00:00', 'regular check up'),
(3, 'Zachary@uncc.edu', 'mnaga@uncc.edu', '2015-10-29', '08:00:00', 'immunizatiom'),
(4, 'mBilliska@uncc.edu', 'mnaga@uncc.edu', '2015-11-10', '10:00:00', 'regular check up'),
(5, 'mBilliska@uncc.edu', 'llopaudr@uncc.edu', '2015-12-09', '12:00:00', 'regular check up');

-- --------------------------------------------------------

--
-- Table structure for table `login`
--

CREATE TABLE IF NOT EXISTS `login` (
  `u_id` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `role` enum('U','D') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `login`
--

INSERT INTO `login` (`u_id`, `password`, `role`) VALUES
('llopaudr@uncc.edu', 'Phoenix131', 'U'),
('mBilliska@uncc.edu', 'Bryan', 'D'),
('mnaga@uncc.edu', 'Nihu*sept21', 'U'),
('Zachary@uncc.edu', 'Reese', 'D');

-- --------------------------------------------------------

--
-- Table structure for table `physician`
--

CREATE TABLE IF NOT EXISTS `physician` (
  `u_id` int(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `u_id` varchar(50) NOT NULL,
  `role` enum('1','2') NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) DEFAULT NULL,
  `dob` date NOT NULL,
  `sex` enum('M','F') NOT NULL,
  `telephone_number` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`u_id`, `role`, `first_name`, `last_name`, `dob`, `sex`, `telephone_number`) VALUES
('llopaudr@uncc.edu', '1', 'Lucy', 'Lopamudra', '1988-12-29', 'F', '3363926556'),
('mnaga@uncc.edu', '1', 'Monisha', 'Naga', '1993-09-21', 'F', '4704298171');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `appointmentbookingdetail`
--
ALTER TABLE `appointmentbookingdetail`
 ADD PRIMARY KEY (`appointment_id`), ADD KEY `physician_id` (`physician_id`), ADD KEY `patient_id` (`patient_id`);

--
-- Indexes for table `login`
--
ALTER TABLE `login`
 ADD PRIMARY KEY (`u_id`);

--
-- Indexes for table `physician`
--
ALTER TABLE `physician`
 ADD PRIMARY KEY (`u_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
 ADD PRIMARY KEY (`u_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `appointmentbookingdetail`
--
ALTER TABLE `appointmentbookingdetail`
MODIFY `appointment_id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=6;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `appointmentbookingdetail`
--
ALTER TABLE `appointmentbookingdetail`
ADD CONSTRAINT `appointmentbookingdetail_ibfk_1` FOREIGN KEY (`physician_id`) REFERENCES `login` (`u_id`),
ADD CONSTRAINT `appointmentbookingdetail_ibfk_2` FOREIGN KEY (`patient_id`) REFERENCES `login` (`u_id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
