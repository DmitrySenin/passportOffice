;angular.module('main', []).factory("PersonalInfoLoader", ['$http', function ($http) {
	
	/**
	 * URL of server.
	 * @type {String}
	 */
	var BASE_URL = "http://localhost:57336/";

	/**
	 * Create URL to API to getting passport data.
	 * @return {string} URL for requesting personal data from server. 
	 */
	function buildPersonalInfoGetURL() {
		return BASE_URL + "personalinfo";
	}

	var PersonalInfoLoader = {};

	/**
	 * Carries out request for getting information from passed URL.
	 * @param  {string} URL Target URL of request.
	 * @return {Promise}     Promise which represents flow of request.
	 */
	PersonalInfoLoader.Load = function() {
		return $http.get(buildPersonalInfoGetURL());
	};

	return PersonalInfoLoader;
}]);