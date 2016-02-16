;(function () {
	angular
		.module('main', ['ui.bootstrap', 'infinite-scroll', 'angularSpinner'])
		.constant('EventNames', {
			'Search' : 'search'
		});
})();