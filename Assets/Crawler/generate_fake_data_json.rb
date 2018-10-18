require "pry"
require "json"
require "pathname"

data = IO.binread("./example-companies-permid.txt")
fake_file = Pathname.new("./data.json")

companies_and_permids = data.split("\n").map do |x|
  company, longpermid = x.split(",")
  shortpermid = longpermid.split("1-").last
  [company, shortpermid.to_i]
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
  data
end

company_data = {
  similarity_score: similarity_data
}

puts "Writing new fake data to: #{fake_file}"
IO.binwrite(fake_file, JSON.generate(company_data))
