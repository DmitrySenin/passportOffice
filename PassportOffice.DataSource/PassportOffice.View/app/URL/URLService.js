;(function() {
	angular
		.module('main')
		.service('URLService', URLService);

	/**
	 * Encapsulates logic of URL service. 
	 */
	function URLService() {

		/**
		 * URL of server.
		 * @type {String}
		 */
		var BASE_URL = "http://localhost:57336/";

		/**
		 * Create URL to API to getting passport data.
		 * @param {Numeric} pageSize Amount of personal information records per one page.
		 * @param {Numeric} pagenumber Number of requested page of date.
		 * @return {string} URL for requesting personal data from server. 
		 */
		this.BuildPersonalInfoGetURL = function(pageSize, pageNumber) {
			return BASE_URL + "personalinfo/" + pageSize + "/" + pageNumber + "/false";
		}
	}
})();