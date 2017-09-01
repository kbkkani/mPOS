-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Aug 31, 2017 at 02:21 PM
-- Server version: 10.1.8-MariaDB
-- PHP Version: 5.6.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `restaurant_gall`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `Products` ()  BEGIN
SELECT categories.id AS ccode,categories.name AS cname,products.id AS pcode,products.name AS pname,product_sizes.price AS pprice FROM products JOIN categories ON categories.id=products.category_id JOIN product_sizes ON product_sizes.products_id=products.id;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `SalesReport` ()  BEGIN
SELECT orders.id AS orderid,orders.created AS orderdate,orders.discount AS orderdiscount,order_details.product_id AS pcode,order_details.unit_price,order_details.qty,order_details.subtotal,@todaysale := (SELECT SUM(order_details.subtotal) FROM order_details WHERE date(order_details.added) = CURDATE()) AS todaysale FROM `order_details` JOIN orders ON orders.id=order_details.order_id ORDER BY orders.created DESC;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `id` int(11) NOT NULL,
  `name` varchar(25) NOT NULL,
  `image` varchar(50) NOT NULL,
  `online` int(11) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`id`, `name`, `image`, `online`) VALUES
(1, 'The Heritage Proprietors ', '0.jpg', 1),
(2, 'Chilled & Bottled Mineral', '0.jpg', 1),
(3, 'Fresh Juices', '0.jpg', 1),
(4, 'Cold Drinks', '0.jpg', 1),
(5, 'Hot Drinks', '0.jpg', 1),
(6, 'Sodas', '0.jpg', 1),
(7, 'Soup Appetizers', '0.jpg', 1),
(8, 'Salads & Sides', '0.jpg', 1),
(9, 'Oven Fired Artisanal Pizz', '0.jpg', 1),
(10, 'Seafood Main', '0.jpg', 1),
(11, 'Heritage Mains', '0.jpg', 1),
(12, 'Kids Menu', '0.jpg', 1),
(13, 'Desserts', '0.jpg', 1),
(14, 'Breakfast ', '0.jpg', 1),
(15, 'testsasasasasasasa', '0.jpg', 1);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `id` int(11) NOT NULL,
  `created` varchar(150) NOT NULL,
  `guest` int(4) DEFAULT '1',
  `tabel` text,
  `priority` varchar(45) DEFAULT NULL,
  `order_type` varchar(1) DEFAULT NULL,
  `paid` double DEFAULT '0',
  `discount` double DEFAULT '0',
  `service_charge` double DEFAULT '0',
  `online` int(11) NOT NULL DEFAULT '1',
  `active` int(1) DEFAULT '0',
  `user_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`id`, `created`, `guest`, `tabel`, `priority`, `order_type`, `paid`, `discount`, `service_charge`, `online`, `active`, `user_id`) VALUES
(1, '2017-08-31 05:43 PM', 2, 'G1', NULL, 'D', 1012, 0, 10, 1, 0, 2),
(2, '2017-08-31 05:44 PM', 2, 'G1', NULL, 'D', 1012, 0, 10, 1, 0, 2),
(3, '2017-08-31 05:45 PM', 2, 'G1', NULL, 'D', 1012, 0, 10, 1, 0, 2),
(4, '2017-08-31 05:45 PM', 2, 'G1', NULL, 'D', 2000, 0, 10, 1, 0, 2);

-- --------------------------------------------------------

--
-- Table structure for table `order_details`
--

CREATE TABLE `order_details` (
  `id` int(11) NOT NULL,
  `product_id` varchar(100) NOT NULL,
  `order_id` int(11) NOT NULL,
  `shift_id` int(11) DEFAULT NULL,
  `shift_no` int(11) DEFAULT NULL,
  `size` varchar(1) DEFAULT NULL,
  `unit_price` double DEFAULT NULL,
  `qty` int(11) NOT NULL,
  `kot_status` int(11) DEFAULT '0',
  `online` int(11) NOT NULL DEFAULT '1',
  `added` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `subtotal` double DEFAULT NULL,
  `item_type` varchar(2) DEFAULT NULL,
  `print_status` int(11) DEFAULT '0',
  `item_spec` varchar(150) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `order_details`
--

INSERT INTO `order_details` (`id`, `product_id`, `order_id`, `shift_id`, `shift_no`, `size`, `unit_price`, `qty`, `kot_status`, `online`, `added`, `subtotal`, `item_type`, `print_status`, `item_spec`) VALUES
(1, '002', 1, 1, 1, 'G', 510, 1, 0, 1, '2017-08-31 12:13:26', 510, '1', 1, NULL),
(2, '001', 1, 1, 1, 'R', 410, 1, 0, 1, '2017-08-31 12:13:26', 410, '2', 1, NULL),
(3, '001', 2, 1, 1, 'R', 410, 1, 0, 1, '2017-08-31 12:14:26', 410, '2', 1, NULL),
(4, '002', 2, 1, 1, 'G', 510, 1, 0, 1, '2017-08-31 12:14:26', 510, '1', 1, NULL),
(5, '001', 3, 1, 1, 'R', 410, 1, 0, 1, '2017-08-31 12:15:06', 410, '2', 1, NULL),
(6, '002', 3, 1, 1, 'G', 510, 1, 0, 1, '2017-08-31 12:15:06', 510, '1', 1, NULL),
(7, '001', 4, 1, 1, 'R', 410, 1, 0, 1, '2017-08-31 12:15:56', 410, '2', 1, NULL),
(8, '002', 4, 1, 1, 'G', 510, 1, 0, 1, '2017-08-31 12:15:56', 510, '1', 1, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `paymentdetails`
--

CREATE TABLE `paymentdetails` (
  `id` int(11) NOT NULL,
  `orders_id` int(11) NOT NULL,
  `cardname` varchar(100) DEFAULT NULL,
  `cardno` varchar(4) DEFAULT NULL,
  `cardtype` varchar(12) DEFAULT NULL,
  `created` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `amount` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `paymentdetails`
--

INSERT INTO `paymentdetails` (`id`, `orders_id`, `cardname`, `cardno`, `cardtype`, `created`, `amount`) VALUES
(1, 1, 'kbk', '1111', 'VISA', '2017-08-31 12:13:46', 1012),
(2, 2, 'kbk', '1212', 'MASTER', '2017-08-31 12:14:33', 1012),
(3, 3, 'kbk', '1212', 'AMEX', '2017-08-31 12:15:16', 1012);

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `id` varchar(100) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `category_id` int(11) NOT NULL,
  `image` varchar(100) DEFAULT NULL,
  `size` varchar(45) DEFAULT NULL,
  `price` double DEFAULT NULL,
  `item_type` varchar(5) DEFAULT NULL,
  `online` int(11) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`id`, `name`, `category_id`, `image`, `size`, `price`, `item_type`, `online`) VALUES
('001', 'Cappuccino ', 1, '0.jpg', 'Regular', 410, '2', 1),
('002', 'cappuccino Grande', 1, '0.jpg', 'Grande ', 510, '1', 1),
('003', 'Cafe Latte', 1, '0.jpg', 'Regular ', 450, '2', 1),
('004', 'Iced Latte', 1, '0.jpg', 'Regular ', 450, '2', 1),
('005', 'Esspresso', 1, '0.jpg', 'Single', 300, '2', 1),
('006', 'Esspresso Dopio', 1, '0.jpg', 'Doppio (Double)', 400, '1', 1),
('007', 'The Heritage Proprietors Cold Brew', 1, '0.jpg', '', 450, '2', 1),
('008', 'Flat White', 1, '0.jpg', '', 420, '2', 1),
('009', 'Cafe Mocha', 1, '0.jpg', 'Regular', 480, '2', 1),
('010', 'Cafe Mocha', 1, '0.jpg', 'Grande', 580, '2', 1),
('011', 'Iced Cafe Mocha', 1, '0.jpg', 'Regular', 480, '2', 1),
('012', 'Americano', 1, '0.jpg', 'Regular', 360, '2', 1),
('013', 'Americano', 1, '0.jpg', 'Grande', 460, '2', 1),
('014', 'Baby Chino', 1, '0.jpg', '', 300, '2', 1),
('015', 'Mineral Water', 2, '0.jpg', 'Large 1000ml (1litre)', 150, '2', 1),
('016', 'Mineral Water medium', 2, '0.jpg', 'Medium 750ml', 130, '2', 1),
('017', 'Mango', 3, '0.jpg', '', 400, '2', 1),
('018', 'Papaya', 3, '0.jpg', '', 400, '2', 1),
('019', 'Pineapple ', 3, '0.jpg', '', 400, '2', 1),
('020', 'Water Melon', 3, '0.jpg', '', 400, '2', 1),
('021', 'Lime', 3, '0.jpg', '', 400, '2', 1),
('023', 'Sri Lankan Fresh Lime & Soda', 4, '0.jpg', '', 400, '2', 1),
('024', 'Fresh King Coconut', 4, '0.jpg', '', 150, '2', 1),
('025', 'Chocolate Milkshake', 4, '0.jpg', '', 600, '2', 1),
('026', 'Banana Milkshake', 4, '0.jpg', '', 600, '2', 1),
('027', 'Vanilla Milkshake', 4, '0.jpg', '', 600, '2', 1),
('028', 'Strawberry Milkshake', 1, '0.jpg', '', 600, '2', 1),
('029', 'Heritage Signature Iced Tea', 4, '0.jpg', '', 300, '2', 1),
('030', 'Moms Secret Chai Iced Tea', 4, '0.jpg', '', 550, '2', 1),
('031', 'Hot Ceylon Tea', 5, '0.jpg', '', 300, '2', 1),
('032', 'Hot Chocolate', 5, '0.jpg', '', 550, '2', 1),
('033', 'Coke', 6, '0.jpg', '', 170, '2', 1),
('034', 'Sprite', 1, '0.jpg', '', 170, '2', 1),
('036', 'Diet Coke', 1, '0.jpg', '', 270, '2', 1),
('037', 'Soda Water', 6, '0.jpg', '', 150, '2', 1),
('040', 'Wild Mushroom Soup', 7, '0.jpg', '', 790, '1', 1),
('041', 'Cream of Tomato Soup', 7, '0.jpg', '', 500, '1', 1),
('042', 'Pumpkin Ginger Soup', 7, '0.jpg', '', 650, '1', 1),
('043', 'Vegetable Spring Rolls', 7, '0.jpg', '', 700, '1', 1),
('044', 'Vietnam Summer Rolls', 7, '0.jpg', '', 1400, '2', 1),
('045', 'Avacado Prawn Cocktail', 7, '0.jpg', '', 1200, '2', 1),
('046', 'Hummus & Pita Bread', 7, '0.jpg', '', 850, '1', 1),
('047', 'Avacado Prawn Salad', 8, '0.jpg', '', 1050, '2', 1),
('048', 'The Heritage Widge Salad', 8, '0.jpg', '', 1200, '2', 1),
('049', 'Salad Nicoise', 8, '0.jpg', '', 1100, '2', 1),
('050', 'Tabbouleh Salad', 8, '0.jpg', '', 850, '2', 1),
('051', 'Geeek Salad', 8, '0.jpg', '', 1100, '2', 1),
('052', 'Crispy French Fries', 8, '0.jpg', '', 500, '1', 1),
('053', 'Potato Wedges', 8, '0.jpg', '', 500, '1', 1),
('060', 'Vegetarian Pizza', 9, '0.jpg', '', 1400, '1', 1),
('061', 'Pizza Margherita', 9, '0.jpg', '', 1300, '1', 1),
('062', 'Seafood Pizza', 9, '0.jpg', '', 2500, '1', 1),
('063', 'Chicken Tandoori Pizza', 9, '0.jpg', '', 1500, '1', 1),
('064', 'Spaghetti Seafood Marinara', 9, '0.jpg', '', 1200, '1', 1),
('065', 'Spaghetti Arabiatta', 9, '0.jpg', '', 950, '1', 1),
('066', 'Pesto Linguni', 9, '0.jpg', '', 950, '1', 1),
('067', 'Spaghetti Bolognese', 9, '0.jpg', '', 1100, '1', 1),
('070', 'Ocean Mixed Grilled Seafood Platter', 10, '0.jpg', '', 2350, '1', 1),
('071', 'Classic Rice & Curry', 10, '0.jpg', '', 1800, '1', 1),
('072', 'Classic Fish & Chips', 10, '0.jpg', '', 1600, '1', 1),
('073', 'Grilled Garlic Prawns', 10, '0.jpg', '', 1800, '1', 1),
('074', 'Herb Marinated Grilled Fish', 10, '0.jpg', '', 1400, '1', 1),
('075', 'Seafood Nasi Goreng', 10, '0.jpg', '', 1700, '1', 1),
('076', 'Spicy Prawn & Vegetable Wrap', 10, '0.jpg', '', 1100, '1', 1),
('077', 'Club Sandwich', 11, '0.jpg', '', 1100, '2', 1),
('078', 'The Heritage Classic Beef Burger', 11, '0.jpg', '', 1400, '1', 1),
('079', 'Spiced Chicken & Vegetable Wrap', 11, '0.jpg', '', 800, '1', 1),
('080', 'Strir Fried Vegetables Platter', 11, '0.jpg', '', 700, '1', 1),
('081', 'Southern Fried Crispy Chicken Strips Ser', 11, '0.jpg', '', 1150, '1', 1),
('082', 'Vegetable Fried Rice', 11, '0.jpg', '', 700, '1', 1),
('090', 'Chicken Fingers', 12, '0.jpg', '', 1000, '1', 1),
('091', 'Spaghetti Marinara', 12, '0.jpg', '', 700, '1', 1),
('092', 'Fish Sticks', 12, '0.jpg', '', 1000, '1', 1),
('101', 'Exotic Fruit Platter', 13, '0.jpg', '', 500, '1', 1),
('102', 'Homemade Banana Cake', 13, '0.jpg', '', 500, '2', 1),
('103', 'Classic Banana Split', 1, '0.jpg', '', 650, '2', 1),
('104', 'Warm Chocolate Brownie', 13, '0.jpg', '', 700, '2', 1),
('105', 'Coconut Crème Brulee', 13, '0.jpg', '', 950, '2', 1),
('106', 'Trio of Ice Cream', 13, '0.jpg', '', 500, '2', 1),
('107', 'Ice Cream By The Scoop', 13, '0.jpg', '', 200, '2', 1),
('2020', 'plastic water bottle', 2, '0.jpg', 'large', 226, '2', 1),
('2021', 'Avacado ', 3, '0.jpg', '', 500, '2', 1),
('2022', 'Mixed fruit juice', 3, '0.jpg', '', 600, '2', 1),
('2023', 'Pita Bread Portion', 8, '0.jpg', '', 500, '1', 1),
('2024', 'Diet Coke', 6, '0.jpg', '', 270, '2', 1),
('2025', 'Sprite', 6, '0.jpg', '', 170, '2', 1),
('2026', 'Fanta', 6, '0.jpg', '', 170, '2', 1),
('2027', 'Classic Banana Solit', 13, '0.jpg', '', 550, '2', 1),
('2030', 'English Breakfast', 14, '0.jpg', '', 1200, '1', 1),
('2031', 'Traditional Sri Lankan Breakfast ', 14, '0.jpg', '', 1300, '1', 1),
('2036', 'Plain Toast Bread', 8, '0.jpg', '', 200, '2', 1),
('22', 'Orange Juice ', 3, '0.jpg', '', 500, '2', 1),
('3032', 'Avacado Toast', 14, '0.jpg', '', 850, '2', 1),
('3033', 'Museli With Banana & Curd', 14, '0.jpg', '', 850, '2', 1),
('3035', 'Pancake Breakfast', 14, '0.jpg', '', 800, '1', 1),
('3040', 'Strawberry Milkshake', 4, '0.jpg', '', 600, '2', 1),
('3041', 'Grilled Vegetable and Paneer Sandwich ', 11, '0.jpg', '', 850, '2', 1),
('35', 'Ginger Beer', 6, '0.jpg', '', 170, '2', 1),
('93', 'Mini Burger', 12, '0.jpg', '', 1000, '1', 1),
('94', 'Mac & Chees ', 12, '0.jpg', '', 800, '1', 1);

-- --------------------------------------------------------

--
-- Table structure for table `shift`
--

CREATE TABLE `shift` (
  `id` int(11) NOT NULL,
  `users_id` int(11) NOT NULL,
  `shift_date` date DEFAULT NULL,
  `shift_start` datetime DEFAULT NULL,
  `shift_end` datetime DEFAULT NULL,
  `shift_no` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `shift`
--

INSERT INTO `shift` (`id`, `users_id`, `shift_date`, `shift_start`, `shift_end`, `shift_no`) VALUES
(1, 2, '2017-08-31', '2017-08-31 17:43:07', NULL, 1);

-- --------------------------------------------------------

--
-- Table structure for table `stock`
--

CREATE TABLE `stock` (
  `id` int(11) NOT NULL,
  `products_id` varchar(100) NOT NULL,
  `price` varchar(45) DEFAULT NULL,
  `stock_in_date` datetime DEFAULT NULL,
  `stock_out_date` datetime DEFAULT NULL,
  `stock_in` int(11) DEFAULT NULL,
  `stock_out` int(11) DEFAULT NULL,
  `user` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `tabeldetails`
--

CREATE TABLE `tabeldetails` (
  `id` int(11) NOT NULL,
  `tabel` varchar(100) DEFAULT NULL,
  `orders_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `table_no`
--

CREATE TABLE `table_no` (
  `id` int(10) UNSIGNED NOT NULL,
  `table_name` varchar(45) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `table_no`
--

INSERT INTO `table_no` (`id`, `table_name`) VALUES
(1, 'G1'),
(2, 'G2'),
(3, 'G3'),
(4, 'G4'),
(5, 'G5'),
(6, 'G6'),
(7, 'G7'),
(8, 'G8'),
(9, 'G9'),
(10, 'G10'),
(11, 'R1'),
(12, 'R2'),
(13, 'R3'),
(14, 'R4'),
(15, 'R5'),
(16, 'R6'),
(17, 'R7'),
(18, 'R8'),
(19, 'R9'),
(20, 'R10'),
(21, 'Balcony 1'),
(22, 'Balcony 2'),
(23, 'Balcony 3'),
(24, 'Think Table'),
(25, 'Sofa 1'),
(26, 'Sofa 2'),
(27, 'Patio 1'),
(28, 'Patio 2'),
(29, 'Patio 3'),
(30, 'Lounge');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(25) NOT NULL,
  `password` varchar(200) NOT NULL,
  `online` int(11) NOT NULL DEFAULT '1',
  `user_type` varchar(1) DEFAULT 'A'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `online`, `user_type`) VALUES
(2, 'lawrence', '71af2da6b34525c317dff4c3044ce973', 1, 'A'),
(3, 'mayuranga', '8df3e10964372d61d6104b39ba0c5ec2', 1, 'W'),
(4, 'chamath', '5c4761dc4a331cf95af8e0cde5c04aa0', 1, 'C'),
(5, 'lakmal', '2d75af3485ae73b6623f9303008df1d4', 1, 'W'),
(6, 'sathiyajith', '76267bef27b1e90daa5729eaa0b6bd11', 1, 'W'),
(7, 'kingsly', '694dc2012c8cc6ab065c76b2c2ba78ed', 1, 'W'),
(8, 'amila', '8b9a9c567ac3b68fa45a7b3ee52725c6', 1, 'W'),
(9, 'lasith', 'aaa29165536c91854c719f81b6371166', 1, 'W');

-- --------------------------------------------------------

--
-- Table structure for table `user_log`
--

CREATE TABLE `user_log` (
  `id` int(11) NOT NULL,
  `created` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `user_id` varchar(45) DEFAULT NULL,
  `event` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user_log`
--

INSERT INTO `user_log` (`id`, `created`, `user_id`, `event`) VALUES
(1, '2017-08-31 12:13:07', '2', 'login'),
(2, '2017-08-31 12:18:12', '2', 'logout');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `order_details`
--
ALTER TABLE `order_details`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_order_details_products1_idx` (`product_id`),
  ADD KEY `fk_order_details_orders1_idx` (`order_id`),
  ADD KEY `fk_order_details_shift1_idx` (`shift_id`);

--
-- Indexes for table `paymentdetails`
--
ALTER TABLE `paymentdetails`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_paymentdetails_orders1_idx` (`orders_id`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id_UNIQUE` (`id`),
  ADD KEY `fk_products_categories_idx` (`category_id`);

--
-- Indexes for table `shift`
--
ALTER TABLE `shift`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_shift_users1_idx` (`users_id`);

--
-- Indexes for table `stock`
--
ALTER TABLE `stock`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_stock_products1_idx` (`products_id`);

--
-- Indexes for table `tabeldetails`
--
ALTER TABLE `tabeldetails`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_tabelDetails_orders1_idx` (`orders_id`);

--
-- Indexes for table `table_no`
--
ALTER TABLE `table_no`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `user_log`
--
ALTER TABLE `user_log`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;
--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `order_details`
--
ALTER TABLE `order_details`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT for table `paymentdetails`
--
ALTER TABLE `paymentdetails`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `shift`
--
ALTER TABLE `shift`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `tabeldetails`
--
ALTER TABLE `tabeldetails`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `table_no`
--
ALTER TABLE `table_no`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `user_log`
--
ALTER TABLE `user_log`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `order_details`
--
ALTER TABLE `order_details`
  ADD CONSTRAINT `fk_order_details_orders1` FOREIGN KEY (`order_id`) REFERENCES `orders` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_order_details_products1` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_order_details_shift1` FOREIGN KEY (`shift_id`) REFERENCES `shift` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `paymentdetails`
--
ALTER TABLE `paymentdetails`
  ADD CONSTRAINT `fk_paymentdetails_orders1` FOREIGN KEY (`orders_id`) REFERENCES `orders` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `fk_products_categories` FOREIGN KEY (`category_id`) REFERENCES `categories` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `shift`
--
ALTER TABLE `shift`
  ADD CONSTRAINT `fk_shift_users1` FOREIGN KEY (`users_id`) REFERENCES `users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `stock`
--
ALTER TABLE `stock`
  ADD CONSTRAINT `fk_stock_products1` FOREIGN KEY (`products_id`) REFERENCES `products` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
