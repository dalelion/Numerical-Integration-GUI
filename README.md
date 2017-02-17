<h1 font-size="24px"><b>Numerical Integration GUI</b> | Project for Calculus II Class</h1>

#Documentation
##Overview
<p font-size="16px">This project is a <b>Numberical Integration Calculator</b> for the function <code>(8x^2 - x^5)</code>.</p>
<p font-size="16px">Currently the user can enter a <em>Lower Bound, Upper Bound</em> and <em>Number of Rectangles</em> to calculate integrals<br /> using three different rules; <em>Midpoint, Trapezoid and Simpson.</em> You can also calculate the Actual value</p>
##Future Plans
<p font-size="16px">The next planned feature is to be able to enter custom functions instead of having a hard-coded function.</p>
<p font-size="16px">This will require Expression parsing and the ability to take the integral of a function through code.</p>


#Comparing Results
####The purpose of this project is to compare the different methods of integration based on their efficiency.
<ul>
	<li>Each rule is fairly accurate and become more accurate when using more rectangles.<br /><br /></li>
	<li>The Midpoint Rule is about twice as accurate as the Trapezoid Rule.<br /><br /></li>
	<li>The Simpson Rule combines the two methods to make it almost exact.<br /><br /></li>
</ul>
##Actual Integral | F(x) = 8x^2 - x^5
	Result="10.666666666666666666666666667"
##Inputs
	Lower Bound="0"
	Upper Bound="2"
	Number of Rectangles="100"
###Midpoint
	Result="10.667466620000"
	% Error="0.007499562500"
###Trapezoid
	Result="10.665066720000"
	% Error="0.014999500"
###Simpson
	Result="10.666666653333333333333333333"
    % Error="0.0000001250000000000000000100"

##Inputs
	Lower Bound="0"
	Upper Bound="2"
	Number of Rectangles="1000"
###<t>Midpoint Rule
	Result="10.666674666662000000"
    % Error="0.0000749999562500"
###Trapezoid Rule
    Result="10.666650666672000000"
    % Error="0.0001499999500"
###Simpson Rule
	Result="10.666666666665333333333333333"
    % Error="0.0000000000125000000000000100"
