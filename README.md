<h1 font-size="24px"><b>Numerical Integration GUI</b> | Project for Calculus II Class</h1>

<h1>Documentation</h1>

<h2>Overview</h2>

<p font-size="16px">This project is a <b>Numberical Integration Calculator</b> for the function <code>(8x^2 - x^5)</code>.</p>
<p font-size="16px">Currently the user can enter a <em>Lower Bound, Upper Bound</em> and <em>Number of Rectangles</em> to calculate integrals<br /> using three different rules; <em>Midpoint, Trapezoid and Simpson.</em> You can also calculate the Actual value</p>

<h2>Future Plans</h2>
<p font-size="16px">The next planned feature is to be able to enter custom functions instead of having a hard-coded function.</p>
<p font-size="16px">This will require Expression parsing and the ability to take the integral of a function through code.</p>

<h1>Comparing Results</h1>

<h4>The purpose of this project is to compare the different methods of integration based on their efficiency.</h4>
<ul>
	<li>Each rule is fairly accurate and become more accurate when using more rectangles.<br /><br /></li>
	<li>The Midpoint Rule is about twice as accurate as the Trapezoid Rule.<br /><br /></li>
	<li>The Simpson Rule combines the two methods to make it almost exact.<br /><br /></li>
</ul>

<h2>Actual Integral | F(x) = 8x^2 - x^5</h2>
	Result="10.666666666666666666666666667"

<h2>Inputs</h2>
<ul>
	<li> Lower Bound="0"</li>
	<li> Upper Bound="2"</li>
	<li> Number of Rectangles="100"</li>
	</ul>

<h3>Midpoint</h3>
<ul>
	<li> Result="10.667466620000" </li>
	<li> % Error="0.007499562500" </li>
	</ul>

<h3>Trapezoid</h3>
<ul>
    <li> Result="10.6650667200000" </li>
    <li> Error="0.014999500" </li>
    </ul>

<h3>Simpson</h3>
<ul>
    <li> Result="10.666666653333333333333333333" </li>
    <li> Error="0.0000001250000000000000000100" </li>
    </ul>

<h2>Inputs</h2>
<ul>
	<li> Lower Bound="0" </li>
	<li> Upper Bound="2" </li>
	<li> Number of Rectangles="1000" </li>
	</ul>

<h3>Midpoint Rule</h3>
<ul>
	<li> Result="10.666674666662000000"</li>
	<li> % Error="0.0000749999562500" </li>
	</ul>

<h3>Trapezoid Rule</h3>
<ul>
    <li> Result="10.6666506666720000000"</li>
    <li> Error="0.0001499999500"</li>
    </ul>

<h3>Simpson Rule</h3>
<ul>
	<li> Result="10.666666666665333333333333333"</li>
    	<li> Error="0.0000000000125000000000000100"</li>
	</ul>
