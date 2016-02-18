;(function () {
	angular
		.module('main', ['ui.bootstrap', 'infinite-scroll', 'angularSpinner'])
		.constant('EventNames', {
			'Search' : 'search'
		})
		.config(function($httpProvider) {
			$httpProvider.interceptors.push('AuthInterceptorService');
		});
})();