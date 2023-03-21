-- phpMyAdmin SQL Dump
-- version 4.9.3
-- https://www.phpmyadmin.net/
--
-- Host: studmysql01.fhict.local
-- Generation Time: Mar 21, 2023 at 09:15 AM
-- Server version: 5.7.26-log
-- PHP Version: 7.4.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbi469729`
--

-- --------------------------------------------------------

--
-- Table structure for table `event`
--

CREATE TABLE `event` (
  `id` int(10) UNSIGNED NOT NULL,
  `start_time` datetime NOT NULL,
  `description` varchar(512) NOT NULL,
  `title` varchar(64) NOT NULL,
  `party_id` int(10) UNSIGNED NOT NULL,
  `picture_url` varchar(512) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `event`
--

INSERT INTO `event` (`id`, `start_time`, `description`, `title`, `party_id`, `picture_url`) VALUES
(1, '2023-03-14 11:52:01', 'Online watchparty', 'Watchparty', 1, 'https://www.google.com/url?sa=i&url=https%3A%2F%2Fvariety.com%2F2021%2Fdigital%2Fnews%2Frick-astley-never-gonna-give-you-up-1-billion-youtube-views-1235030404%2F&psig=AOvVaw0c6hDCPV2L-xJELpNGcmNy&ust=1678877718151000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCODRiIKh2_0CFQAAAAAdAAAAABAE'),
(2, '2023-03-15 11:52:01', 'OAMK - Finland', 'Oamk watchparty', 2, 'https://www.google.com/url?sa=i&url=https%3A%2F%2Fvariety.com%2F2021%2Fdigital%2Fnews%2Frick-astley-never-gonna-give-you-up-1-billion-youtube-views-1235030404%2F&psig=AOvVaw0c6hDCPV2L-xJELpNGcmNy&ust=1678877718151000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCODRiIKh2_0CFQAAAAAdAAAAABAE');

-- --------------------------------------------------------

--
-- Table structure for table `movie`
--

CREATE TABLE `movie` (
  `id` int(10) UNSIGNED NOT NULL,
  `imdb_id` varchar(64) NOT NULL,
  `votes` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `movie`
--

INSERT INTO `movie` (`id`, `imdb_id`, `votes`) VALUES
(1, '1', 0),
(2, '2', 0);

-- --------------------------------------------------------

--
-- Table structure for table `movieevent`
--

CREATE TABLE `movieevent` (
  `movie_id` int(11) UNSIGNED NOT NULL,
  `event_id` int(11) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `movieevent`
--

INSERT INTO `movieevent` (`movie_id`, `event_id`) VALUES
(1, 1),
(2, 2);

-- --------------------------------------------------------

--
-- Table structure for table `movieuser`
--

CREATE TABLE `movieuser` (
  `movie_id` int(10) UNSIGNED NOT NULL,
  `user_id` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `movieuser`
--

INSERT INTO `movieuser` (`movie_id`, `user_id`) VALUES
(1, 1),
(2, 2);

-- --------------------------------------------------------

--
-- Table structure for table `party`
--

CREATE TABLE `party` (
  `id` int(10) UNSIGNED NOT NULL,
  `title` varchar(64) NOT NULL,
  `picture_url` varchar(512) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `party`
--

INSERT INTO `party` (`id`, `title`, `picture_url`) VALUES
(1, 'betere ', 'https://www.vectorstock.com/royalty-free-vector/simple-teamwork-group-of-three-people-human-vector-20120951'),
(2, 'oamk', 'https://www.vectorstock.com/royalty-free-vector/simple-teamwork-group-of-three-people-human-vector-20120951'),
(3, 'ewre', 'werwe');

-- --------------------------------------------------------

--
-- Table structure for table `partyuser`
--

CREATE TABLE `partyuser` (
  `party_id` int(10) UNSIGNED NOT NULL,
  `user_id` int(10) UNSIGNED NOT NULL,
  `partymanager` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `partyuser`
--

INSERT INTO `partyuser` (`party_id`, `user_id`, `partymanager`) VALUES
(1, 1, 0),
(2, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(64) NOT NULL,
  `password` varchar(64) DEFAULT NULL,
  `email` varchar(64) DEFAULT NULL,
  `registered_user` tinyint(1) NOT NULL DEFAULT '0',
  `picture_url` varchar(512) NOT NULL DEFAULT 'https://imgur.com/gallery/YZlcj',
  `role` varchar(64) NOT NULL DEFAULT 'user',
  `confirmation_link` varchar(512) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `name`, `password`, `email`, `registered_user`, `picture_url`, `role`, `confirmation_link`) VALUES
(1, 'Mike', 'admin', 'mikeymike', 0, 'https://imgur.com/gallery/YZlcj', 'user', NULL),
(2, 'Mikkel', 'admin', 'MAIKUL', 1, 'https://imgur.com/gallery/YZlcj', 'user', NULL),
(3, 'tewtwe', 'wetwet', 'wetwet', 0, 'https://imgur.com/gallery/YZlcj', 'user', NULL),
(4, 'admin', 'root', 'admin@groopyswoopy.com', 0, 'https://imgur.com/gallery/YZlcj', 'user', NULL),
(5, 'testuser', 'testpassword', 'testmail', 0, 'https://imgur.com/gallery/YZlcj', 'user', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `event`
--
ALTER TABLE `event`
  ADD PRIMARY KEY (`id`),
  ADD KEY `group_id` (`party_id`);

--
-- Indexes for table `movie`
--
ALTER TABLE `movie`
  ADD PRIMARY KEY (`id`),
  ADD KEY `imdb_id` (`imdb_id`);

--
-- Indexes for table `movieevent`
--
ALTER TABLE `movieevent`
  ADD KEY `movie_id` (`movie_id`),
  ADD KEY `event_id` (`event_id`);

--
-- Indexes for table `movieuser`
--
ALTER TABLE `movieuser`
  ADD KEY `movie_id` (`movie_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `party`
--
ALTER TABLE `party`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `partyuser`
--
ALTER TABLE `partyuser`
  ADD KEY `user_id` (`user_id`),
  ADD KEY `group_id` (`party_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `event`
--
ALTER TABLE `event`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `movie`
--
ALTER TABLE `movie`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `party`
--
ALTER TABLE `party`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `event`
--
ALTER TABLE `event`
  ADD CONSTRAINT `event_ibfk_1` FOREIGN KEY (`id`) REFERENCES `party` (`id`),
  ADD CONSTRAINT `group_id` FOREIGN KEY (`party_id`) REFERENCES `party` (`id`);

--
-- Constraints for table `movieevent`
--
ALTER TABLE `movieevent`
  ADD CONSTRAINT `movieevent_ibfk_1` FOREIGN KEY (`event_id`) REFERENCES `event` (`id`),
  ADD CONSTRAINT `movieevent_ibfk_2` FOREIGN KEY (`movie_id`) REFERENCES `movie` (`id`);

--
-- Constraints for table `movieuser`
--
ALTER TABLE `movieuser`
  ADD CONSTRAINT `movieuser_ibfk_1` FOREIGN KEY (`movie_id`) REFERENCES `movie` (`id`),
  ADD CONSTRAINT `movieuser_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`);

--
-- Constraints for table `partyuser`
--
ALTER TABLE `partyuser`
  ADD CONSTRAINT `partyuser_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`),
  ADD CONSTRAINT `partyuser_ibfk_2` FOREIGN KEY (`party_id`) REFERENCES `party` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
