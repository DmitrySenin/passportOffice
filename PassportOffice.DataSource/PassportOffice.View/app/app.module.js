;(function () {
	angular
		.module('main', ['ui.bootstrap', 'infinite-scroll'])
		.constant('EventNames', {
			'Search' : 'search'
		});
})();