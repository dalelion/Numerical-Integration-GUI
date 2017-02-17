# Numerical-Integration-GUI | <span font-size="13px">Project for Calculus II Class</span>

####The porpose of this project is to compare the different methods of integration based on their efficiency.
	Each rule is fairly accurate and become more accurate when using more rectangles.
	
	The Midpoint Rule is about twice as accurate as the Trapezoid Rule.
	
	The Simpson Rule combines the two methods to make it almost exact.

#Comparing Results
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
