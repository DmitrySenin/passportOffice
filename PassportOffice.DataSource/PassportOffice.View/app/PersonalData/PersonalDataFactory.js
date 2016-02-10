;angular.module('main', ['ui.bootstrap']).factory("PersonalInfoLoader", ['$http', function ($http) {
	
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
	 * Create instance of object represents searching criteria.
	 * @return {object} Object which fields contatins restirction of searching.
	 */
	PersonalInfoLoader.SearchingOptions = function () {
		this.FirstName = null;
		this.LastName = null;
		this.MiddleName = null;
		this.BirthdayDate = null;
		this.PassportSeries = null;
		this.PassportNumber = null;
	};

	/**
	 * Carries out request for getting information from passed URL.
	 * @param  {string} URL Target URL of request.
	 * @return {Promise}     Promise which represents flow of request.
	 */
	PersonalInfoLoader.Load = function(searchingOptions) {
		return $http.get(buildPersonalInfoGetURL(), { params : searchingOptions });
	};

	return PersonalInfoLoader;
}]);