require "pry"
require "json"
require "pathname"

data = IO.binread("./example-companies-permid.txt")
fake_file = Pathname.new("./data.json")
metadata = JSON.parse(IO.binread("./company-metadata-emp-rev.json"))

companies_and_permids = data.split("\n").map do |x|
  company, permid = x.split(",https://permid.org/1-")
  [company, permid]
end

permids = companies_and_permids.map(&:last)

similarity_data = companies_and_permids.map do |company, permid|
  data = Hash.new
  data["id"] = permid
  data["name"] = company
  data["similarities"] = permids.each_with_object({}) do |child_permid, hash|
    next if permid == child_permid
    hash[child_permid] = rand
  end
  %w[num_employees amt_revenue amt_revenue_year].each do |extra_attr|
    data[extra_attr] = metadata[permid][extra_attr]
  end
  data
end

company_data = {
  similarity_score: similarity_data
}

puts "Writing new fake data to: #{fake_file}"
IO.binwrite(fake_file, JSON.generate(company_data))
