;(function() {
	angular
		.module('main')
		.factory('AuthInterceptorService', AuthInterceptorService);

	AuthInterceptorService.$inject = ['AuthenticationInfoStorage'];

	function AuthInterceptorService(AuthenticationInfoStorage) {
		return {
			request : Request
		};

		/**
		 * Manage all request and authorization header to it.
		 * @param {Object} config Configuration of request.
		 */
		function Request(config) {
			config.headers = config.headers || {};

			if(AuthenticationInfoStorage.IsAuthenticated) {
				config.headers.Authorization = 'Bearer ' + AuthenticationInfoStorage.AccessToken;
			}

			return config;
		}
	}
})();